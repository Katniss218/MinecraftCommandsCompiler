using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using MCC.OutputLanguage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCC
{
    class Program
    {
        static void Main( string[] args )
        {
            List<MCCFile> parsedFiles = new List<MCCFile>();

            Parser parser = new Parser();

            string dirName = "_Project";

            string dirPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + dirName;

            string[] filesToParse = Directory.GetFiles( dirPath, "*.mcc", SearchOption.AllDirectories );

            foreach( var filePath in filesToParse )
            {
                string relativePath = Path.GetRelativePath( dirPath, filePath );

                string fileContents = File.ReadAllText( filePath, Encoding.UTF8 );

                parser.SetFile( relativePath, fileContents );
                MCCFile file = parser.Parse();
                parsedFiles.Add( file );
            }

            foreach( var f in parsedFiles )
            {
                LanguageTransformer.Simplify( f );
            }

            CodeGenerator codeGen = new CodeGenerator(parsedFiles);

            codeGen.GenerateOutput( AppDomain.CurrentDomain.BaseDirectory, "testDatapack" );
        }
    }
}
