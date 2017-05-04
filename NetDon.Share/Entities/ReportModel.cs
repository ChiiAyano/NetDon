using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class ReportModel
    {
        /// <summary>
        /// Get the report ID.
        /// レポート ID を取得します。
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; internal set; }

        /// <summary>
        /// Get action taken int response to the report.
        /// このレポートに対してアクションがおこなわれたかどうかを取得します。
        /// </summary>
        [JsonProperty("action_token")]
        public bool ActionTaken { get; internal set; }

        public override string ToString()
        {
            return $"[{this.ID}] Action: {this.ActionTaken}";
        }
    }
}
