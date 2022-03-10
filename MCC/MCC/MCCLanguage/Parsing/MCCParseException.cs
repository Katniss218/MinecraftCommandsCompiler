using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Parsing
{
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
