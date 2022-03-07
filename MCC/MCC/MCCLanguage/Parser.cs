﻿using MCC.MCCLanguage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage
{
    /// <summary>
    /// A parser for the MCCLanguage. Outputs the MCCLanguage Object Representation.
    /// </summary>
    public class Parser
    {
        public string FilePath { get; private set; }

        public string S { get; private set; }

        public int Pos { get; private set; }

        public char CurrentChar
        {
            get
            {
                if( Pos < 0 || Pos >= S.Length )
                {
                    throw new Exception( "Out of bounds" );
                }
                return S[Pos];
            }
        }


        public Parser()
        {

        }

        public void SetFile( string filePath, string fileContents )
        {
            this.FilePath = filePath;
            this.S = fileContents;
            this.Pos = 0;
        }

        public MCCFile Parse()
        {
            return EatFile( FilePath );
        }

        public bool IsValidIdentifierChar( char c )
        {
            switch( c )
            {
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'r':
                case 'q':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '_':
                case '.':
                    return true;
                default:
                    return false;
            }
        }

        public void EatWhiteSpaces(bool includeNewLines = true)
        {
            try
            {
                while( char.IsWhiteSpace( CurrentChar ) && ((CurrentChar != '\r' && CurrentChar != '\n') || includeNewLines) )
                {
                    Pos++;
                }
            }
            catch
            {
                // files can end with as many whitespaces as we want.
                return;
            }
        }

        public void EatKeyword( string keyword )
        {
            for( int i = 0; i < keyword.Length; i++ )
            {
                if( CurrentChar != keyword[i] )
                {
                    throw new Exception( $"Invalid character '{CurrentChar}' at {Pos}" );
                }

                Pos++;
            }
        }

        public string EatAttribute()
        {
            // attribute
            //  : '[' identifier ']'
            //  ;

            EatKeyword( "[" );

            string attrName = EatIdentifier();

            EatKeyword( "]" );

            return attrName;
        }

        public string EatIdentifier()
        {
            // identifier = [a-z0-9_.]

            StringBuilder sb = new StringBuilder();

            while( IsValidIdentifierChar( CurrentChar ) )
            {
                sb.Append( CurrentChar );
                Pos++;
            }

            return sb.ToString();
        }

        public void EatNewLine()
        {
            // new_line
            //  : '\n'
            //  | '\r\n'
            //  ;

            try
            {
                EatKeyword( "\r" );
            }
            catch
            {
                // linux doesn't have \r
            }

            EatKeyword( "\n" );
        }

        public MCCCommand EatCommand()
        {
            // command
            //  : char* ~new_line
            //  : char* 'run' new_line whitespace* '{' function_body '}'
            //  ;

            StringBuilder sb = new StringBuilder();

#warning refactor this to not be reliant on the newline
            while( CurrentChar != '\n' && CurrentChar != '\r' )
            {
                sb.Append( CurrentChar );
                Pos++;
            }

            string cmd = sb.ToString();

            if( cmd.EndsWith( "run" ) )
            {
                EatNewLine();

                EatWhiteSpaces();

                EatKeyword( "{" );

                MCCFunction.FunctionBody body = EatFunctionBody();

                EatKeyword( "}" );

                cmd = sb.ToString();

                return new MCCCommand() { RawCommand = cmd, InlineFunctionBody = body };
            }

            return new MCCCommand() { RawCommand = cmd };
        }

        public MCCFunction.FunctionBody EatFunctionBody()
        {
            // function_body
            //  : (whitespace* command whitespace*)*
            //  ;

            MCCFunction.FunctionBody body = new MCCFunction.FunctionBody();
            while( CurrentChar != '}' )
            {
                EatWhiteSpaces();

                MCCCommand command = EatCommand();

                EatWhiteSpaces();

                body.Commands.Add( command );
            }

            return body;
        }

        public MCCFunction EatFunction()
        {
            // function
            //  : (attribute whitespace*)* 'function' whitespace* identifier whitespace* '{' function_body '}'
            //  ;

            MCCFunction func = new MCCFunction();

            while( CurrentChar == '[' )
            {
                string attr = EatAttribute();

                EatWhiteSpaces();

                if( attr == "load" || attr == "onload" )
                {
                    func.Load = true;
                }
                else if( attr == "tick" || attr == "ontick" )
                {
                    func.Tick = true;
                }
                else
                {
                    throw new Exception( $"Unknown attribute '{attr}'" );
                }
            }

            EatKeyword( "function" );

            EatWhiteSpaces();

            string identifier = EatIdentifier();

            EatWhiteSpaces();

            EatKeyword( "{" );

            MCCFunction.FunctionBody body = EatFunctionBody();

            EatKeyword( "}" );

            func.Body = body;
            func.Identifier = identifier;
            func.Load = false;
            func.Tick = false;

            return func;
        }

        public MCCFile EatFile( string filePath )
        {
            // file
            //  : whitespace* 'namespace' whitespace* identifier whitespace* new_line (whitespace* function whitespace*)*
            //  ;

            EatWhiteSpaces();

            EatKeyword( "namespace" );

            EatWhiteSpaces();

            string @namespace = EatIdentifier();

            EatWhiteSpaces(false);
            EatNewLine();

            MCCFile file = new MCCFile( @namespace, filePath );

            while( Pos < S.Length )
            {
                EatWhiteSpaces();

                MCCFunction func = EatFunction();
                file.Functions.Add( func );

                EatWhiteSpaces();
            }

            return file;
        }
    }
}