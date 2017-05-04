using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class CardModel
    {
        [JsonProperty("url")]
        public Uri Url { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("image")]
        public string Image { get; internal set; }

        public override string ToString()
        {
            return
                $"Title: {this.Title}, Description: {this.Description}, URL: {this.Url}, Image: {this.Image}";
        }
    }
}
