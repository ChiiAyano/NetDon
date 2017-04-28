using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class StatusModel
    {
        /// <summary>
        /// Get a toot ID.
        /// トゥート ID を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }
        /// <summary>
        /// Get a Fediverse-unique resource ID.
        /// Fediverse ライクな識別 ID を取得します。
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; internal set; }
        /// <summary>
        /// Get a toot URL.
        /// このトゥートの URL を取得します。
        /// </summary>
        [JsonProperty("url")]
        public Uri TootUrl { get; internal set; }
        /// <summary>
        /// Get an information of tooted account.
        /// このトゥートをしたアカウントの情報を取得します。
        /// </summary>
        [JsonProperty("account")]
        public AccountModel Account { get; internal set; }
        /// <summary>
        /// Get the ID of the status it replies to.
        /// どのトゥート ID に対するリプライかを取得します。
        /// </summary>
        [JsonProperty("in_reply_to_id")]
        public long? InReplyToId { get; internal set; }
        /// <summary>
        /// Get the ID of the account it reply to.
        /// どのアカウント ID に対するリプライかを取得します。
        /// </summary>
        [JsonProperty("in_reply_to_account_id")]
        public long? InReplyToAccountId { get; internal set; }
        /// <summary>
        /// Get boosted toot.
        /// ブースト元トゥート情報を取得します。
        /// </summary>
        [JsonProperty("reblog")]
        public StatusModel Reblog { get; internal set; }
        /// <summary>
        /// Get a status (HTML contains.)
        /// トゥート内容を取得します。HTML タグが使用されています。
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; internal set; }
        /// <summary>
        /// Get a date of tooted.
        /// トゥートした日時を取得します。
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }
        /// <summary>
        /// Get a boosts count.
        /// ブースト数を取得します。
        /// </summary>
        [JsonProperty("reblogs_count")]
        public int ReblogsCount { get; internal set; }
        /// <summary>
        /// Get a favorites count.
        /// お気に入り数を取得します。
        /// </summary>
        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; internal set; }
        /// <summary>
        /// Get the boosted by authenticated user (use this client user.)
        /// 認証したユーザー (クライアント利用者) がブーストしたかどうかを取得します。
        /// </summary>
        [JsonProperty("reblogged", NullValueHandling = NullValueHandling.Ignore)]
        public bool Reblogged { get; internal set; }
        /// <summary>
        /// Get the favorited by authenticated user (use this client user.)
        /// 認証したユーザー (クライアント利用者) がお気に入りに登録したかどうかを取得します。
        /// </summary>
        [JsonProperty("favourited", NullValueHandling = NullValueHandling.Ignore)]
        public bool Favourited { get; internal set; }
        /// <summary>
        /// Get the sensitive of images in this toot.
        /// このトゥートに含まれる画像が、閲覧注意かどうかを取得します。
        /// </summary>
        [JsonProperty("sensitive")]
        public bool Sensitive { get; internal set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("spoiler_text")]
        public string SpoilerTest { get; internal set; }
        /// <summary>
        /// Get the visibility of toot.
        /// このトゥートの表示範囲を取得します。
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; internal set; }
        /// <summary>
        /// Get the app used to toot.
        /// このトゥートをする際に使用したアプリの情報を取得します。
        /// </summary>
        [JsonProperty("application")]
        public ApplicationModel ApplicationInfo { get; internal set; }
        /// <summary>
        /// Get mentioned user informations.
        /// メンションしたユーザーの情報を取得します。
        /// </summary>
        [JsonProperty("mentions")]
        public MentionModel[] Mentions { get; internal set; }
        /// <summary>
        /// Get media attachments.
        /// 添付されたメディアの情報を取得します。
        /// </summary>
        [JsonProperty("media_attachments")]
        public AttachmentModel[] MediaAttachments { get; internal set; }
        /// <summary>
        /// Get hashtags.
        /// トゥート内に含まれるハッシュタグの情報を取得します。
        /// </summary>
        [JsonProperty("tags")]
        public TagModel[] Tags { get; internal set; }

        public override string ToString()
        {
            // Display Name の設定がないユーザーがいる
            var userName = string.IsNullOrWhiteSpace(this.Account.DisplayName) ? this.Account.UserName : this.Account.DisplayName;

            return
                $"[{this.ID}] {userName}: {this.Content} ({this.CreatedAt.ToLocalTime():yyyy/MM/dd HH:mm:ss zzz})";
        }
    }
}
