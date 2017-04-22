using Newtonsoft.Json;

namespace NetDon.Entities
{
    public class AppModel
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        public override string ToString()
        {
            return $"[ClientId] {this.ClientId} [ClientSecret] {this.ClientSecret}";
        }
    }
}
