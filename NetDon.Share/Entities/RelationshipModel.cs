using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class RelationshipModel
    {
        /// <summary>
        /// Get an account ID.
        /// アカウント ID を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }

        /// <summary>
        /// Get this user follwed by authenticated user.
        /// このユーザーをフォローしているかどうかを取得します。
        /// </summary>
        [JsonProperty("following")]
        public bool Following { get; internal set; }

        /// <summary>
        /// Get an authenticated user followed by this user.
        /// このユーザーが認証済みユーザーをフォローしているかどうかを取得します。
        /// </summary>
        [JsonProperty("followed_by")]
        public bool FollowedBy { get; internal set; }

        /// <summary>
        /// Get this user blocked by authenticated user.
        /// このユーザーをブロックしているかどうかを取得します。
        /// </summary>
        [JsonProperty("blocking")]
        public bool Blocking { get; internal set; }

        /// <summary>
        /// Get this user muted by authenticated user.
        /// このユーザーをミュートしているかどうかを取得します。
        /// </summary>
        [JsonProperty("muting")]
        public bool Muting { get; internal set; }

        /// <summary>
        /// Get requested to follow the account by authenticated user.
        /// このユーザーに対してフォロー リクエストを送信している状態かどうかを取得します。
        /// </summary>
        [JsonProperty("requested")]
        public bool Requested { get; internal set; }

        public override string ToString()
        {
            return $"[{this.ID}] Following: {this.Following}, Followed: {this.FollowedBy}, Blocking: {this.Blocking}, Muting: {this.Muting}, Follow Requested: {this.Requested}";
        }
    }
}
