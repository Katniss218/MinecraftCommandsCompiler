using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    public class MCCFunction
    {
        public class FunctionBody
        {
            public List<MCCCommand> Commands { get; set; } = new List<MCCCommand>();
#warning todo - prevent duplicate names
        }

        public string Identifier { get; set; }

        public FunctionBody Body { get; set; }

        public bool Load { get; set; }
        public bool Tick { get; set; }
    }
}
