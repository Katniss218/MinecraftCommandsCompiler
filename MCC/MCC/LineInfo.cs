using System;
using System.Collections.Generic;
using System.Text;

namespace MCC
{
    public class LineInfo
	{
		public string FileName { get; set; }

		public int LineNo { get; set; }

        public int ColNo { get; set; }

		public LineInfo( string fileName, int lineNo, int colNo )
		{
			this.FileName = fileName;
			this.LineNo = lineNo;
			this.ColNo = colNo;
		}

		public override string ToString()
		{
			return "'" + this.FileName + "' - line: " + this.LineNo + ", col: " + this.ColNo;
		}

		public static LineInfo Calculate( string fileName, string s, int pos )
		{
			int newLineChars = 1; // beginning at line no. 1, not 0
			int charsSinceNewLine = 1; // beginning at col no. 1, not 0
			string newLine = Environment.NewLine;
			for( int i = 0; i < pos; i++ )
			{
				charsSinceNewLine++;
				if( s.Substring( i, newLine.Length ) == newLine )
				{
					i += newLine.Length;
					newLineChars++;
					charsSinceNewLine = 0;
				}
			}
			return new LineInfo( fileName, newLineChars, charsSinceNewLine );
		}
	}
}
