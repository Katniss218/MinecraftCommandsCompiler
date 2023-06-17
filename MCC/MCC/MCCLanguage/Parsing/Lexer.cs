using System.Collections.Generic;

namespace MCC.MCCLanguage.Parsing
{
    public class Lexer
    {
        private string _filePath;
        private string _s;
        private int _pos;

        public Lexer()
        {

        }

        public void SetFile( string filePath, string fileContents )
        {
            this._filePath = filePath;
            this._s = fileContents;
            this._pos = 0;
        }


        protected char PeekChar()
        {
            return _s[_pos];
        }

        protected char PeekChar( int offset )
        {
            return _s[_pos + offset];
        }

        protected int Advance( int step = 1 )
        {
            int begin = _pos;
            _pos += step;
            return begin;
        }

        private string GetTokenValue( int begin )
        {
            return _s[begin.._pos];
        }

        private List<SyntaxToken> LexFirstPass()
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>();

            int begin = _pos;

            while( _pos < _s.Length - 1 )
            {
                if( PeekChar() == '{' )
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.OpenCurlyBracket, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                if( PeekChar() == '}' )
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.CloseCurlyBracket, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                if( PeekChar() == '[' )
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.OpenSquareBracket, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                if( PeekChar() == ']' )
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.CloseSquareBracket, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                if( PeekChar() == '\n' )
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.NewLine, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                else if( PeekChar() == '\r' && PeekChar( 1 ) == '\n' )
                {
                    begin = Advance( 2 );


                    tokens.Add( new SyntaxToken( SyntaxTokenType.NewLine, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
                else if( PeekChar() == '/' )
                {
                    begin = Advance();

                    if( PeekChar() == '/' )
                    {
                        Advance();
                        tokens.Add( new SyntaxToken( SyntaxTokenType.DoubleSlash, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                    }
                    else if( PeekChar() == '*' )
                    {
                        Advance();
                        tokens.Add( new SyntaxToken( SyntaxTokenType.SlashAsterisk, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                    }
                    else
                    {
                        tokens.Add( new SyntaxToken( SyntaxTokenType.Slash, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                    }
                }
                else
                {
                    begin = Advance();

                    tokens.Add( new SyntaxToken( SyntaxTokenType.Slash, GetTokenValue( begin ), LineInfo.Calculate( _filePath, _s, begin ) ) );
                }
            }
            return tokens;
        }

        public List<SyntaxToken> LexSecondPass( List<SyntaxToken> inTokens )
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>();
            // recursively combine tokens until there is nothing changing.

            List<SyntaxToken> currentTokens = new List<SyntaxToken>(); // the tokens that we will combine in this iteration.

            foreach( var token in inTokens )
            {
                if( token.Type == SyntaxTokenType.Text )
                {

                }
            }
        }

        public List<SyntaxToken> Lex()
        {
            List<SyntaxToken> tokens = LexFirstPass();


        }
    }
}
