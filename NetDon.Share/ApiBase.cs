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

        protected HttpClient CreateClient()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);

            return http;
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

        protected async Task<T> GetAsync<T>(Uri endpoint)
        {
            var http = CreateClient();

            var response = await http.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(result);
                return data;
            }

            return default(T);
        }
    }
}
