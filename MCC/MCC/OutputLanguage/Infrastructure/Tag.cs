using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MCC.OutputLanguage.Infrastructure
{
    /// <summary>
    /// Represents the Minecraft block, item, function, etc tag json file
    /// </summary>
    public class Tag
    {
        [JsonProperty( "replace" )]
        public bool? Replace { get; set; }

        [JsonProperty("values")]
        public List<string> Values { get; set; } = new List<string>();

        // possible todo - values can contain an object (string 'id', bool 'required') instead
    }
}
