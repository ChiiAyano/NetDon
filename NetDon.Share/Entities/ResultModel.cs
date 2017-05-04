using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDon.Entities
{
    public class ResultModel
    {
        /// <summary>
        /// Get result search matched accounts.
        /// 検索に一致したアカウントを取得します。
        /// </summary>
        [JsonProperty("accounts")]
        public IEnumerable<AccountModel> Accounts { get; internal set; }

        /// <summary>
        /// Get result search matched statues.
        /// 検索に一致したトゥートを取得します。
        /// </summary>
        [JsonProperty("statuses")]
        public IEnumerable<StatusModel> Statues { get; internal set; }

        /// <summary>
        /// Get result search matched hashtags.
        /// 検索に一致したハッシュタグを取得します。
        /// </summary>
        [JsonProperty("hashtags")]
        public IEnumerable<string> Hashtags { get; internal set; }

        public override string ToString()
        {
            return
                $"Accounts ({this.Accounts.Count()} results): {string.Join(", ", this.Accounts.Select(s => "[" + s.ID + "]"))}, " +
                $"Statues ({this.Statues.Count()} results): {string.Join(", ", this.Statues.Select(s => "[" + s.ID + "]"))}, " +
                $"Hashtags: {string.Join(", ", this.Hashtags)}";
        }
    }
}
