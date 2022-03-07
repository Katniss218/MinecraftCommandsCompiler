using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    public class MCCFile
    {
        public string Namespace { get; set; } = "test";

        public string FilePath { get; set; }

        public List<MCCFunction> Functions { get; set; } = new List<MCCFunction>();

        public MCCFile( string @namespace, string filePath )
        {
            Namespace = @namespace;
            FilePath = filePath;
        }

        public void AddFunction( MCCFunction func )
        {
            if( Functions.Find( f => f.Identifier == func.Identifier ) != null )
            {
                throw new Exception( $"Duplicated function name '{func.Identifier}' in file '{FilePath}'" );
            }

            Functions.Add( func );
        }
    }
}
