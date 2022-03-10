using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Parsing
{
    public class SyntaxToken
    {
        /// <summary>
        /// Describes the position at which the token starts.
        /// </summary>
        public LineInfo LineInfo { get; set; }

        public SyntaxTokenType Type { get; set; }

        /// <summary>
        /// String representation of the token.
        /// </summary>
        public string Value { get; set; }

        public SyntaxToken( SyntaxTokenType type, string value, LineInfo lineInfo )
        {
            this.LineInfo = lineInfo;
            this.Type = type;
            this.Value = value;
        }
    }
}
