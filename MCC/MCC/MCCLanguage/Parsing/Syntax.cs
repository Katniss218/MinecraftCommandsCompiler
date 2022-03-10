using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Parsing
{
    public static class Syntax
    {
        public static bool IsValidIdentifierChar( char c )
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

    }
}
