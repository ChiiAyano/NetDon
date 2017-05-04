using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDon.Entities
{
    public class ContextModel
    {
        [JsonProperty("ancestors")]
        public IEnumerable<StatusModel> Ancestors { get; internal set; }

        [JsonProperty("descendants")]
        public IEnumerable<StatusModel> Descendants { get; internal set; }

        public override string ToString()
        {
            return
                $"Ancestors ({this.Ancestors.Count()} items), Descendants ({this.Descendants.Count()} items)";
        }
    }
}
