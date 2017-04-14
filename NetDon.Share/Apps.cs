using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using NetDon.Models;

namespace NetDon
{
    public class Apps : ApiBase
    {
        [Flags]
        public enum Scope
        {
            Read =      0x00,
            Write =     0x01,
            Follow =    0x10
        }

        public static string DefaultRedirectUri { get; } = "urn:ietf:wg:oauth:2.0:oob";

        public async Task<AppModel> RegisterAppAsync(string mastdonUri, string clientName, string redirectUri, Scope scopes, string webSite)
        {
            return await RegisterAppAsync(mastdonUri, clientName, redirectUri, GetScopes(scopes), webSite);
        }

        public async Task<AppModel> RegisterAppAsync(string mastdonUri, string clientName, string redirectUri, string scopes, string webSite)
        {
            return await RegisterAppAsync(new Uri(mastdonUri), clientName, redirectUri, scopes, webSite);
        }

        public async Task<AppModel> RegisterAppAsync(Uri mastdonUri, string clientName, string redirectUri, string scopes, string webSite)
        {
            var uri = CreateUriBase(mastdonUri, "app");
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("client_name", clientName),
                new KeyValuePair<string,string>("redirect_uris", redirectUri),
                new KeyValuePair<string,string>("scopes", scopes),
                new KeyValuePair<string,string>("website", webSite)
            });

            var result = await new HttpClient().PostAsync(uri, data);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var registData = JsonConvert.DeserializeObject<AppModel>(response);

                return registData;
            }

            return null;
        }

        private string GetScopes(Scope scopes)
        {
            var result = new List<string>();
            if ((scopes & Scope.Read) == Scope.Read) result.Add("read");
            if ((scopes & Scope.Write) == Scope.Write) result.Add("write");
            if ((scopes & Scope.Follow) == Scope.Follow) result.Add("follow");

            return string.Join(" ", result);
        }
    }
}
