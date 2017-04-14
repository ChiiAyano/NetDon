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
            var requestData = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string> ("client_id", this.clientId),
                new KeyValuePair<string, string> ("client_secret", this.clientSecret),
                new KeyValuePair<string, string> ("grant_type", "password"),
                new KeyValuePair<string, string> ("username", email),
                new KeyValuePair<string, string> ("password", password)
            });

            var response = await new HttpClient().PostAsync(uri, requestData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
            return null;
        }

        #endregion
    }
}
