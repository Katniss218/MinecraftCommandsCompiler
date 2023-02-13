using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    /// <summary>
    /// Represents a single .mcc script file.
    /// </summary>
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
    }
}
