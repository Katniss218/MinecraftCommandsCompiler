using MCC.MCCLanguage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage
{
    /// <summary>
    /// Performs 'Lowering' on the mcc language. Expresses complex mcc concepts with simpler ones. The result is fed to the code generator.
    /// </summary>
    public static class LanguageLowerer
    {
        static void SimplifyMarkStep( MCCFile file, MCCFunction func, MCCCommand command, ref List<MCCCommandMapping> inlineCommands )
        {
            if( command.InlineFunctionBody != null )
            {
                MCCCommandMapping commandMapping = new MCCCommandMapping( file, func, command );

                inlineCommands.Add( commandMapping );

                foreach( var cmd in command.InlineFunctionBody.Commands )
                {

                    SimplifyMarkStep( file, func, cmd, ref inlineCommands );
                }
            }
        }

        public static void Simplify( MCCFile file )
        {
            List<MCCCommandMapping> inlineCommands = new List<MCCCommandMapping>();
            // this could be appended to the file metadata later.

            foreach( var func in file.Functions )
            {
                foreach( var command in func.Body.Commands )
                {
                    SimplifyMarkStep( file, func, command, ref inlineCommands );

                }
            }

            int i = 0;
            foreach( var command in inlineCommands )
            {
                // generate a function for each inline command

                MCCFunction newFunc = new MCCFunction();
                newFunc.Identifier = $"{command.Func.Identifier}.gen_{i}";
                newFunc.Body = command.Cmd.InlineFunctionBody;

                file.Functions.Add( newFunc );

#warning todo - normalization inside parser?
                command.Cmd.RawCommand = command.Cmd.RawCommand.TrimEnd(); // normalize the command so there's always only one space between it and the function keyword.
                command.Cmd.RawCommand += " function " + newFunc.Identifier; // append the new function to the command, the code gen will know what to do with it.
                i++;
            }
        }
    }
}
