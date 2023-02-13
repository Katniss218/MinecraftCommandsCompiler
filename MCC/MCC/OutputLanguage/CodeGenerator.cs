using MCC.MCCLanguage.Infrastructure;
using MCC.OutputLanguage.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCC.OutputLanguage
{
    /// <summary>
    /// Generates mcfunction files.
    /// </summary>
    public class CodeGenerator
    {
        const char NAMESPACED_ID_DIRECTORY_SEPARATOR = '/';

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
                return $"{namespaceWords[0]}:{string.Join( NAMESPACED_ID_DIRECTORY_SEPARATOR, namespaceWords[1..] )}/{functionName}";
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
                    + Path.DirectorySeparatorChar + string.Join( NAMESPACED_ID_DIRECTORY_SEPARATOR, namespaceWords[1..] )
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
#warning TODO - currently doesn't support two functions with the same id within different namespaces - use the transformer to force fully qualified names always. then convert those.
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

        public static string ToJson( object obj )
        {
            return JsonConvert.SerializeObject( obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            } );
        }

        public static void WriteFile( string filePath, string contents )
        {
            string dirPath = Path.GetDirectoryName( filePath );
            if( !Directory.Exists( dirPath ) )
            {
                Directory.CreateDirectory( dirPath );
            }

            File.WriteAllText( filePath, contents, new UTF8Encoding( false ) );
        }

        /// <summary>
        /// Main method - compiles the MCC project into a datapack.
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="datapackName"></param>
        public void GenerateOutput( string outputPath, string datapackName )
        {
#warning todo - generator tests
            string datapackPath = outputPath
                + datapackName;

            McMeta pack = new McMeta()
            {
                Pack = new McMeta.PackData()
                {
                    PackFormat = 6,
                    Description = "test datapack"
                }
            };

            WriteFile( datapackPath + Path.DirectorySeparatorChar + "pack.mcmeta", ToJson( pack ) );

            string dataPath = datapackName
                + Path.DirectorySeparatorChar + "data";

            string minecraftNamespacePath = dataPath
                + Path.DirectorySeparatorChar + "minecraft";

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

                    /*

                    'datapack_name'
                        'data'
                            <namespace>
                                'functions'
                                    ...
                            'minecraft'
                                'tags'
                                    'functions'
                                        ...

                    */

                    string minecraftPath = GetMinecraftPath( file.Namespace, func.Identifier );

                    string filePath = dataPath
                        + Path.DirectorySeparatorChar + minecraftPath;

                    string absolutePath = outputPath + Path.DirectorySeparatorChar + filePath;
                    
                    WriteFile( absolutePath, fileContents );
                }
            }

            Tag functionTagTick = new Tag();
            Tag functionTagLoad = new Tag();

            string minecraftFuncAbsPath = outputPath + minecraftNamespacePath + Path.DirectorySeparatorChar + "tags" + Path.DirectorySeparatorChar + "functions";

            var funcsTick = Proj.Mappings.Where( m => m.Func.Tick );
            foreach( var funcTick in funcsTick )
            {
                functionTagTick.Values.Add( funcTick.GetFullyQualifiedIdentifier() );
            }

            var funcsLoad = Proj.Mappings.Where( m => m.Func.Load );
            foreach( var funcLoad in funcsLoad )
            {
                functionTagLoad.Values.Add( funcLoad.GetFullyQualifiedIdentifier() );
            }

            string tickTagPath = minecraftFuncAbsPath + Path.DirectorySeparatorChar + "tick.json";
            WriteFile( tickTagPath, ToJson( functionTagTick ) );

            string loadTagPath = minecraftFuncAbsPath + Path.DirectorySeparatorChar + "load.json";
            WriteFile( loadTagPath, ToJson( functionTagLoad ) );
        }
    }
}
