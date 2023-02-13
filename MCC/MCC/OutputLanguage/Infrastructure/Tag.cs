using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MCC.OutputLanguage.Infrastructure
{
    /// <summary>
    /// Represents a single tag JSON file (tags contain groups of resources, like items, entities, functions, etc).
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
