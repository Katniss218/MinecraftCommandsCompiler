using MCC.MCCLanguage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC
{
    public class MCCFunctionMapping
    {
        public string Namespace { get; set; }
        public string FilePath { get; set; }

        public MCCFunction Func { get; set; }

        public MCCFunctionMapping( MCCFile file, MCCFunction function )
        {
            this.Namespace = file.Namespace;
            this.FilePath = file.FilePath;
            this.Func = function;
        }
    }

    public class MCCCommandMapping
    {
        public string Namespace { get; set; }
        public string FilePath { get; set; }
        public MCCFunction Func { get; set; }

        public MCCCommand Cmd { get; set; }

        public MCCCommandMapping( MCCFile file, MCCFunction function, MCCCommand command )
        {
            this.Namespace = file.Namespace;
            this.FilePath = file.FilePath;
            this.Func = function;
            this.Cmd = command;
        }
    }

    public class Mapper
    {
        /// <summary>
        /// returns a list of all functions in the given project and their locations
        /// </summary>
        public static List<MCCFunctionMapping> GetFunctionMap( List<MCCFile> files )
        {
            // each function maps to a (string @namespace, string filePath, string functionName)
            List<MCCFunctionMapping> mappings = new List<MCCFunctionMapping>();

            foreach( var file in files )
            {
                foreach( var func in file.Functions )
                {
                    MCCFunctionMapping map = new MCCFunctionMapping( file, func );

                    mappings.Add( map );
                }
            }

            return mappings;
        }
    }
}
