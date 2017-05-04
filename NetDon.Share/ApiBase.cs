using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetDon
{
    public abstract class ApiBase
    {
        private const string prefix = "/api/v1";
        protected readonly Uri instanceUri;
        private readonly string accessToken;

        HttpClient httpClient;

        public ApiBase(string instanceUri, string accessToken)
        {
            this.instanceUri = new Uri(instanceUri);
            this.accessToken = accessToken;
        }

        public ApiBase(Uri instanceUri, string accessToken)
        {
            this.instanceUri = instanceUri;
            this.accessToken = accessToken;
        }

        protected ApiBase(Uri instanceUri)
        {
            this.instanceUri = instanceUri;
        }

        protected HttpClient CreateClient(bool requireAuth = true)
        {
            if (this.httpClient == null) this.httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = requireAuth ? new AuthenticationHeaderValue("Bearer", this.accessToken) : null;

            return this.httpClient;
        }

        protected Uri CreateUriBase(string endpoint)
        {
            return CreateUriBase(this.instanceUri, endpoint);
        }

        protected Uri CreateUriBase(Uri baseUri, string endpoint)
        {
            var uri = new Uri(baseUri, prefix + endpoint);
            return uri;
        }

        protected string CreateGetParameters(params Expression<Func<object, object>>[] exprs)
        {
            var contents = new StringBuilder();
            foreach (var expr in exprs)
            {
                var obj = expr.Compile()?.Invoke(null);
                if (obj == null) continue;

                var param = obj.ToString();
                if (string.IsNullOrWhiteSpace(param)) continue;

                contents.Append($"{expr.Parameters[0].Name}={Uri.EscapeUriString(param)}&");
            }

            return contents.ToString().Trim('&');
        }

        protected async Task<T> GetAsync<T>(Uri endpoint, bool requireAuth = true)
        {
            var http = CreateClient(requireAuth);

            var response = await http.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(result);
                return data;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
            }

            return default(T);
        }
    }
}
