using MCC.MCCLanguage.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCC.OutputLanguage
{
    public class CodeGenerator
    {
        /// <summary>
        /// Returns the namespaced ID used to call an .mcfunction file from a datapack
        /// </summary>
        /// <remarks>
        /// namespace.namespace2.namespace3.fileName.mcc
        /// namespace:namespace2/namespace3/functionName
        /// </remarks>
        public static string GetMinecraftNamespacedId( string @namespace, string functionName )
        {
            string[] namespaceWords = @namespace.Split( '.' );

            if( namespaceWords.Length == 1 )
            {
                return $"{namespaceWords[0]}:{functionName}";
            }
            else
            {
                return $"{namespaceWords[0]}:{string.Join( "/", namespaceWords[1..] )}/{functionName}";
            }
        }

        /// <summary>
        /// Returns the path to an .mcfunction file inside the datapack
        /// </summary>
        public static string GetMinecraftPath( string @namespace, string functionName )
        {
            string[] namespaceWords = @namespace.Split( '.' );

            if( namespaceWords.Length == 1 )
            {
                return namespaceWords[0]
                    + Path.DirectorySeparatorChar + "functions"
                    + Path.DirectorySeparatorChar + functionName + ".mcfunction";
            }
            else
            {
                return namespaceWords[0]
                    + Path.DirectorySeparatorChar + "functions"
                    + Path.DirectorySeparatorChar + string.Join( "/", namespaceWords[1..] )
                    + Path.DirectorySeparatorChar + functionName + ".mcfunction";
            }
        }

        public string ProcessCommand( string @namespace, string fileName, string functionName, string command )
        {
            // the parameters describe where the command is, not what it calls.
            // -------------
            // replace function identifier calls with their appropriate namespaced ids.

            int index = command.IndexOf( "function" );
            if( index != -1 )
            {
                int startIndex = index + "function ".Length;
                string beginning = command[0..startIndex];
                string calleeName = command[startIndex..];

                // don't replace actual valid minecraft function calls :"D
                if( !calleeName.Contains( ':' ) )
                {
                    // find where the function being called actually is.
                    MCCFunctionMapping mapping = this.Proj.Mappings.Find( n => n.Func.Identifier == calleeName );
#warning TODO - currently doesn't support two functions with the same id within different namespaces.
                    return beginning + GetMinecraftNamespacedId( mapping.Namespace, mapping.Func.Identifier );
                }
            }

            return command;
        }

        public MCCProject Proj { get; set; }

        public CodeGenerator( MCCProject proj )
        {
            this.Proj = proj;
        }

        public void GenerateOutput( string outputPath, string datapackName )
        {
#warning todo - add the functions marked with Tick and Load to the respective tags.
            foreach( var file in this.Proj.Files )
            {
                foreach( var func in file.Functions )
                {
                    StringBuilder sb = new StringBuilder();
                    if( func.Body.Commands.Count > 0 )
                    {
                        string cmd = ProcessCommand( file.Namespace, file.FilePath, func.Identifier, func.Body.Commands[0].RawCommand );
                        sb.Append( cmd );

                        for( int i = 1; i < func.Body.Commands.Count; i++ )
                        {
                            sb.Append( Environment.NewLine );

                            cmd = ProcessCommand( file.Namespace, file.FilePath, func.Identifier, func.Body.Commands[i].RawCommand );
                            sb.Append( cmd );
                        }
                    }

                    string fileContents = sb.ToString();

                    // function namespace:filename/identifier/nestedidentifiers/etc/etc

                    /*

                    datapack_name
                        data
                            namespace
                                functions
                                    ...
                                tags
                                    functions
                                        ...

                    */

                    string path = GetMinecraftPath( file.Namespace, func.Identifier );

                    string filePath = datapackName
                        + Path.DirectorySeparatorChar + "data"
                        + Path.DirectorySeparatorChar + path;

                    string absolutePath = outputPath + Path.DirectorySeparatorChar + filePath;
                    string absoluteDirPath = Path.GetDirectoryName( absolutePath );

                    if( !Directory.Exists( absoluteDirPath ) )
                    {
                        Directory.CreateDirectory( absoluteDirPath );
                    }

                    File.WriteAllText( absolutePath, fileContents, Encoding.UTF8 );
                }
                // for each function, generate an mcfunction file.
                // safe the mcfunction file.
            }
        }
    }
}
