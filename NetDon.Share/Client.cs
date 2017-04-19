using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetDon
{
    public class Client : ApiBase
    {
        private readonly Uri instanceUri;
        private readonly string clientId;
        private readonly string clientSecret;

        public Client(string instanceUri, string clientId, string clientSecret)
        {
            this.instanceUri = new Uri(instanceUri);
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public Client(Uri instanceUri, string consumerId, string consumerSecret)
        {
            this.instanceUri = instanceUri;
            this.clientId = consumerId;
            this.clientSecret = consumerSecret;
        }

        #region GetToken

        /// <summary>
        /// Get an Authorize URI.
        /// 認証に使用する URI を取得します。UWP の場合はこの URI を WebAuthenticationBroker などで使用することにより、アクセス トークンを取得できます。
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public Uri GetAuthorizeUri(string redirectUri)
        {
            var parameter = "oauth/authorize?client_id=" + this.clientId + "&response_type=code&redirect_uri=" + Uri.EscapeUriString(redirectUri);

            return new Uri(instanceUri, parameter);
        }

        #endregion
    }
}
