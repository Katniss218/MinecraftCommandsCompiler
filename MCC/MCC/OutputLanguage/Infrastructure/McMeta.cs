using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.OutputLanguage.Infrastructure
{
    public class McMeta
    {
        public class PackData
        {
            [JsonProperty( "pack_format" )]
            public int PackFormat { get; set; }

            [JsonProperty( "description" )]
            public string Description { get; set; }
        }

        [JsonProperty( "pack" )]
        public PackData Pack { get; set; }
    }
}
