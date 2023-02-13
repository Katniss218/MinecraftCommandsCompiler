using System;
using System.Collections.Generic;
using System.Text;

namespace MCC
{
    /// <summary>
    /// An exception that is thrown when the process of compiling an MCC project into a datapack fails.
    /// </summary>
    public class MCCException : Exception
    {
        public MCCException() : base()
        {

        }

        public MCCException( string message ) : base( message )
        {

        }

        public MCCException( string message, Exception innerException ) : base( message, innerException )
        {

        }
    }
}
