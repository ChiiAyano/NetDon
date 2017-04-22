using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Entities
{
    public class ApplicationModel
    {
        /// <summary>
        /// Get a Name of the app. アプリ名を取得します。
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Get a Web Site URL of the app. アプリの紹介 URL を取得します。
        /// </summary>
        public Uri Website { get; internal set; } 
    }
}
