using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using NetDon.Models;
using NetDon.Enums;

namespace NetDon
{
    public class Apps : ApiBase
    {
        public static string DefaultRedirectUri { get; } = "urn:ietf:wg:oauth:2.0:oob";

        public async Task<AppModel> RegisterAppAsync(string mastdonUri, string clientName, string redirectUri, Scope scopes, string webSite)
        {
            return await RegisterAppAsync(mastdonUri, clientName, redirectUri, scopes.ToScopeStrings(), webSite);
        }

        public async Task<AppModel> RegisterAppAsync(string mastdonUri, string clientName, string redirectUri, string scopes, string webSite)
        {
            return await RegisterAppAsync(new Uri(mastdonUri), clientName, redirectUri, scopes, webSite);
        }

        public async Task<AppModel> RegisterAppAsync(Uri mastdonUri, string clientName, string redirectUri, string scopes, string webSite)
        {
            var uri = CreateUriBase(mastdonUri, "apps");
            var data = new Dictionary<string, string>
            {
                {"client_name", clientName },
                { "redirect_uris", redirectUri },
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
