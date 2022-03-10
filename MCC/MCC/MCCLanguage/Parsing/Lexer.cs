using System.Collections.Generic;

namespace MCC.MCCLanguage.Parsing
{
    public class Lexer
    {
        private List<SyntaxToken> _tokens;

        private string _fileName;
        private string _string;
        private int _pos;

        public Lexer()
        {

        }

        public void Reset( string fileName, string stringToLex )
        {
            this._tokens = new List<SyntaxToken>();
            this._fileName = fileName;
            this._string = stringToLex;
        }


        protected char PeekChar()
        {
            return _string[_pos];
        }

        protected char PeekChar( int offset )
        {
            return _string[_pos + offset];
        }

        protected int Advance( int step = 1 )
        {
            int begin = _pos;
            _pos += step;
            return begin;
        }

        private string GetTokenValue( int begin )
        {
            return _string[begin.._pos];
        }

        public void Lex()
        {
            int begin = _pos;

            while( _pos < _string.Length - 1 )
            {
                if( PeekChar() == '{' )
                {
                    begin = Advance();

                    _tokens.Add( new SyntaxToken( SyntaxTokenType.OpenCurlyBracket, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                if( PeekChar() == '}' )
                {
                    begin = Advance();

                    _tokens.Add( new SyntaxToken( SyntaxTokenType.CloseCurlyBracket, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                if( PeekChar() == '[' )
                {
                    begin = Advance();

                    _tokens.Add( new SyntaxToken( SyntaxTokenType.OpenSquareBracket, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                if( PeekChar() == ']' )
                {
                    begin = Advance();

                    _tokens.Add( new SyntaxToken( SyntaxTokenType.CloseSquareBracket, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                if( PeekChar() == '\n' )
                {
                    begin = Advance();


                    _tokens.Add( new SyntaxToken( SyntaxTokenType.NewLine, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                else if( PeekChar() == '\r' && PeekChar( 1 ) == '\n' )
                {
                    begin = Advance( 2 );


                    _tokens.Add( new SyntaxToken( SyntaxTokenType.NewLine, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                }
                else if( PeekChar() == '/' )
                {
                    begin = Advance();

                    if( PeekChar() == '/' )
                    {
                        Advance();
                        _tokens.Add( new SyntaxToken( SyntaxTokenType.DoubleSlash, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                    }
                    else if( PeekChar() == '*' )
                    {
                        Advance();
                        _tokens.Add( new SyntaxToken( SyntaxTokenType.SlashAsterisk, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                    }
                    else
                    {
                        _tokens.Add( new SyntaxToken( SyntaxTokenType.Slash, GetTokenValue( begin ), LineInfo.Calculate( _fileName, _string, begin ) ) );
                    }
                }
                // todo - multi-step tokens.
                // scan text
            }
        }
    }
}
