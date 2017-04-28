using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class TagModel
    {
        /// <summary>
        /// Get a hashtag without #.
        /// # を除いたハッシュタグ名を取得します。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }
        /// <summary>
        /// Get the URL of the hashtag.
        /// ハッシュタグの URL を取得します。
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; internal set; }

        public override string ToString()
        {
            return $"#{this.Name} ({this.Url})";
        }
    }
}
