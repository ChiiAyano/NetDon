using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class AccountModel
    {
        /// <summary>
        /// Get an Account ID.
        /// アカウント ID を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }
        /// <summary>
        /// Get a User Name.
        /// ユーザー名を取得します。
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; internal set; }
        /// <summary>
        /// Get a User Name. If in other Instances, domain name included.
        /// 別のインスタンス ユーザーの場合、ドメインを含んだユーザー名を取得します。
        /// </summary>
        [JsonProperty("acct")]
        public string Acct { get; internal set; }
        /// <summary>
        /// Get a user display name.
        /// ユーザーの表示名を取得します。
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        /// <summary>
        /// Get a user had locked (hidden user).
        /// このユーザーが非公開かどうかを取得します。
        /// </summary>
        [JsonProperty("locked")]
        public bool Locked { get; internal set; }
        /// <summary>
        /// Get a user created date.
        /// このユーザーの作成日時を取得します。
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }
        /// <summary>
        /// Get the count of follower users.
        /// このユーザーをフォローしている数を取得します。
        /// </summary>
        [JsonProperty("followers_count")]
        public long FollowersCount { get; internal set; }
        /// <summary>
        /// Get the count of following users.
        /// このユーザーがフォローしている数を取得します。
        /// </summary>
        [JsonProperty("following_count")]
        public long FollowingCount { get; set; }
        /// <summary>
        /// Get the count of tooted in this account.
        /// このユーザーがトゥートした数を取得します。
        /// </summary>
        [JsonProperty("statuses_count")]
        public long StatusesCount { get; set; }
        /// <summary>
        /// Get a biography of user.
        /// このユーザーの自己紹介を取得します。
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; internal set; }
        /// <summary>
        /// Get a user URL.
        /// このユーザーの URL を取得します。
        /// </summary>
        [JsonProperty("url")]
        public Uri AccountUrl { get; set; }
        /// <summary>
        /// Get an avatar image URL.
        /// このユーザーのアバター画像 URL を取得します。
        /// </summary>
        [JsonProperty("avatar")]
        public Uri AvatarImageUrl { get; internal set; }
        /// <summary>
        /// Get a header image URL.
        /// このユーザーのヘッダー画像 URL を取得します。
        /// </summary>
        [JsonProperty("header")]
        public Uri HeaderImageUrl { get; internal set; }

        public override string ToString()
        {
            return
                $"[ID] {this.ID} (@{this.UserName} : @{this.Acct} {(this.Locked ? "🔒" : string.Empty)}), [Follow/Follower] {this.FollowingCount}/{this.FollowersCount}, " +
                $"[Toot] {this.StatusesCount}";
        }
    }
}
