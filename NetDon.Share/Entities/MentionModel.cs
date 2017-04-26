using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class MentionModel
    {
        /// <summary>
        /// Get URL of user's Profile.
        /// ユーザー情報の URL を取得します。
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; internal set; }
        /// <summary>
        /// Get a User Name. If in other Instances, domain name included.
        /// 別のインスタンス ユーザーの場合、ドメインを含んだユーザー名を取得します。
        /// </summary>
        [JsonProperty("acct")]
        public string Acct { get; internal set; }
        /// <summary>
        /// Get account ID.
        /// アカウント名を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }
        /// <summary>
        /// Get the username of the account.
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; internal set; }

        public override string ToString()
        {
            return $"[ID: {this.ID}] @{this.Acct} ({this.UserName})";
        }
    }
}
