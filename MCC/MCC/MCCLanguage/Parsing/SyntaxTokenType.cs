using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Parsing
{
    public enum SyntaxTokenType
    {
        /// <summary>
        /// '['
        /// </summary>
        OpenSquareBracket,

        /// <summary>
        /// ']'
        /// </summary>
        CloseSquareBracket,

        /// <summary>
        /// '{'
        /// </summary>
        OpenCurlyBracket,

        /// <summary>
        ///  '}'
        /// </summary>
        CloseCurlyBracket,

        /// <summary>
        /// ' ', '\t'
        /// </summary>
        WhiteSpace,

        /// <summary>
        /// '\n' or '\r\n'
        /// </summary>
        NewLine,

        /// <summary>
        /// '.'
        /// </summary>
        Dot,

        /// <summary>
        /// '/'
        /// </summary>
        Slash,

        /// <summary>
        /// '//'
        /// </summary>
        DoubleSlash,

        /// <summary>
        /// '/*'
        /// </summary>
        SlashAsterisk,

        /// <summary>
        /// '*/'
        /// </summary>
        AsteriskSlash,

        /// <summary>
        /// Any text
        /// </summary>
        Text,
    }
}
