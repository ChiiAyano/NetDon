using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class NotificationModel
    {
        /// <summary>
        /// Get the notification ID.
        /// 通知 ID を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }

        /// <summary>
        /// Get the notification type.
        /// 通知の種類を取得します。
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; internal set; }

        /// <summary>
        /// Get a date of notification.
        /// この通知が発行された日時を取得します。
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }

        /// <summary>
        /// Get a user of sent notification.
        /// この通知を送信したユーザーの情報を取得します。
        /// </summary>
        [JsonProperty("account")]
        public AccountModel Account { get; internal set; }

        /// <summary>
        /// If applicable, get notification toot.
        /// 利用可能な場合、通知対象のトゥートを取得します。
        /// </summary>
        [JsonProperty("status")]
        public StatusModel Status { get; internal set; }

        public override string ToString()
        {
            return $"[{this.ID}] Type: {this.Type}, User: {this.Account.UserName}, Status: {this.Status?.Content ?? "None"}";
        }
    }
}
