using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using NetDon.Entities;
using NetDon.Enums;

namespace NetDon
{
    public class Apps : ApiBase
    {
        public static string DefaultRedirectUri { get; } = "urn:ietf:wg:oauth:2.0:oob";

        public Apps(Uri mastodonUrl)
            : base(mastodonUrl)
        {
        }

        public async Task<AppModel> RegisterAppAsync(string clientName, string redirectUrl, Scope scopes, string webSite)
        {
            return await RegisterAppAsync(clientName, redirectUrl, scopes.ToScopeStrings(), webSite);
        }

        public async Task<AppModel> RegisterAppAsync(string clientName, string redirectUrl, string scopes, string webSite)
        {
            var uri = CreateUriBase("apps");
            var data = new Dictionary<string, string>
            {
                {"client_name", clientName },
                { "redirect_uris", redirectUrl },
                { "scopes", scopes },
            };

            if (!string.IsNullOrWhiteSpace(webSite))
            {
                data.Add("website", webSite);
            }

            var content = new FormUrlEncodedContent(data);

            var result = await new HttpClient().PostAsync(uri, content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var registData = JsonConvert.DeserializeObject<AppModel>(response);

                return registData;
            }

            return null;
        }
    }
}
