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
            MCCProject proj = new MCCProject();

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

            proj.Files = parsedFiles;
            proj.Mappings = Mapper.GetFunctionMapAndValidate( proj.Files ); // validate round 1

            foreach( var f in parsedFiles )
            {
                LanguageTransformer.Simplify( f );
            }

            proj.Mappings = Mapper.GetFunctionMapAndValidate( proj.Files ); // re-map to allow calling the generated functions.

            CodeGenerator codeGen = new CodeGenerator( proj );

            codeGen.GenerateOutput( AppDomain.CurrentDomain.BaseDirectory, "testDatapack" );
        }
    }
}
