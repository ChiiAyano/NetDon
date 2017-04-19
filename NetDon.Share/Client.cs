using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetDon
{
    public class Client : ApiBase
    {
        private readonly Uri instanceUri;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string accessToken;

        public Client(string instanceUri, string clientId, string clientSecret)
            : this(new Uri(instanceUri), clientId, clientSecret)
        {
        }

        public Client(string instanceUri, string clientId, string clientSecret, string accessToken)
            : this(instanceUri, clientId, clientSecret)
        {
            this.accessToken = accessToken;
        }

        public Client(Uri instanceUri, string clientId, string clientSecret)
        {
            this.instanceUri = instanceUri;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public Client(Uri instanceUri, string clientId, string clientSecret, string accessToken)
            : this(instanceUri, clientId, clientSecret)
        {
            this.accessToken = accessToken;
        }

        #region Create Client

        private HttpClient CreateClient()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);

            return http;
        }

        #endregion

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

        #region Accounts

        /// <summary>
        /// Get an Current User Information. 現在ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentUserAsync()
        {
            var endpoint = "/api/v1/accounts/verify_credentials";
            var http = CreateClient();

            var response = await http.GetAsync(new Uri(this.instanceUri, endpoint));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        #endregion

        #region Timelines

        public async Task<string> GetHomeTimelineAsync()
        {
            var endpoint = "/api/v1/timelines/home";
            var http = CreateClient();

            var response = await http.GetAsync(new Uri(this.instanceUri, endpoint));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        #endregion
    }
}
