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

        // TODO 戻り値型は一時的
        public async Task<string> GetAccessTokenAsync(string email, string password)
        {
            var uri = new Uri(instanceUri, "oauth/token");
            var requestData = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id", this.clientId },
                    { "client_secret", this.clientSecret },
                    { "grant_type", "password" },
                    { "username", email },
                    { "password", password }
                });

            var response = await new HttpClient().PostAsync(uri, requestData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
            return null;
        }

        public Uri GetAuthorizeUri(string redirectUri)
        {
            var parameter = "?client_id=" + this.clientId + "&response_type=code&redirect_uri=" + redirectUri;

            return new Uri(instanceUri, parameter);
        }

        #endregion
    }
}
