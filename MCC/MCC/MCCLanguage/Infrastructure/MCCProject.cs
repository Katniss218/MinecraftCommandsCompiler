using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.MCCLanguage.Infrastructure
{
    /// <summary>
    /// Represents an MCC project.
    /// </summary>
    /// <remarks>
    /// An MCC project consists of multiple MCC (.mcc) files that contain the code/data.
    /// </remarks>
    public class MCCProject
    {
        public List<MCCFile> Files { get; set; } = new List<MCCFile>();

        public List<MCCFunctionMapping> Mappings { get; set; }
    }
}
