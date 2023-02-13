﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    /// <summary>
    /// Represents a single script function in the MCC language.
    /// </summary>
    public class MCCFunction
    {
        public class FunctionBody
        {
            public List<MCCCommand> Commands { get; set; } = new List<MCCCommand>();
        }

        public string Identifier { get; set; }

        public FunctionBody Body { get; set; }

        public bool Load { get; set; }
        public bool Tick { get; set; }
    }
}
