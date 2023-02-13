using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Parsing
{
    /// <summary>
    /// An exception that is thrown during parsing of the MCC file.
    /// </summary>
    public class MCCParseException : MCCException
    {
        public MCCParseException() : base()
        {

        }

        public MCCParseException( string message ) : base( message )
        {

        }

        public MCCParseException( string message, Exception innerException ) : base( message, innerException )
        {

        }
    }
}
