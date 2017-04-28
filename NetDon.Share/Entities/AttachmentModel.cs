using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class AttachmentModel
    {
        [JsonProperty("id")]
        public long ID { get; internal set; }
        [JsonProperty("remote_url")]
        public Uri RemoteUrl { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("url")]
        public Uri Url { get; internal set; }
        [JsonProperty("preview_url")]
        public Uri PreviewUrl { get; internal set; }
        [JsonProperty("text_url")]
        public Uri TextUrl { get; internal set; }

        public override string ToString()
        {
            return $"[{this.ID}] Type: {this.Type} Url: {this.Url}";
        }
    }
}
