using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    public class MCCProject
    {
        public List<MCCFile> Files { get; set; } = new List<MCCFile>();

        public List<MCCFunctionMapping> Mappings { get; set; }
    }
}
