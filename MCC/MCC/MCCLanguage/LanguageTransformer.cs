using MCC.MCCLanguage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage
{
    /// <summary>
    /// Performs transformation on the language to bring it to the simplest form. To a form that code gen can use.
    /// </summary>
    public class LanguageTransformer
    {
        public static void Simplify( MCCFile file )
        {
            List<MCCCommandMapping> inlineCommands = new List<MCCCommandMapping>();
            // this could be appended to the file metadata later.


            foreach( var func in file.Functions )
            {
                foreach( var command in func.Body.Commands )
                {
                    if( command.InlineFunctionBody != null )
                    {
                        MCCCommandMapping commandMapping = new MCCCommandMapping( file, func, command );

                        inlineCommands.Add( commandMapping );
                    }
                }
            }

            int i = 0;
            foreach( var command in inlineCommands )
            {
                // generate a function for each inline command

                MCCFunction newFunc = new MCCFunction();
                newFunc.Identifier = $"{command.Func.Identifier}.gen_{i}";
                newFunc.Body = command.Cmd.InlineFunctionBody;

                file.AddFunction( newFunc );

                command.Cmd.RawCommand += "function " + newFunc.Identifier; // append the new function to the command, the code gen will know what to do with it.
                i++;
            }
        }
    }
}
