using System;
using System.Collections.Generic;
using System.Text;

namespace MCC
{
    public class MCCException : Exception
    {
        public MCCException() : base()
        {

        }

        public MCCException( string message ) : base(message)
        {

        }

        public MCCException( string message, Exception innerException ) : base( message, innerException )
        {

        }
    }
}
