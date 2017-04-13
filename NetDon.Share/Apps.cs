using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        protected override string ApiEndpointName { get; } = "apps";

        public static async Task RegisterAppAsync(string mastdonUri, string clientName, string redirectUri, Scope scopes, string webSite)
        {
            var uri = CreateUriBase(new Uri(mastdonUri));
            
        }
    }
}
