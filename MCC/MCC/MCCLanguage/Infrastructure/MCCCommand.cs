using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    public class MCCCommand
    {
        public string RawCommand { get; set; }

        /// <summary>
        /// If it's not null, then the command is an execute with an inline function attached.
        /// </summary>
        public MCCFunction.FunctionBody InlineFunctionBody { get; set; }
    }
}
