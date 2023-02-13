using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    /// <summary>
    /// Represents a single MCC script command. Roughly equivalent to a minecraft command.
    /// </summary>
    public class MCCCommand
    {
        public string RawCommand { get; set; }

        // TODO - parse arguments.

        /// <summary>
        /// If it's not null, then the command is an execute with an inline function attached.
        /// </summary>
        public MCCFunction.FunctionBody InlineFunctionBody { get; set; }
    }
}
