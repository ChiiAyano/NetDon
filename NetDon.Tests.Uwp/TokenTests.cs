
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace NetDon.Tests.Uwp
{
    [TestClass]
    public class TokenTests : NetDonTestClient
    {
        [TestMethod]
        public async Task Authentication()
        {
            var client = GetClient();
            var uri = client.GetAuthorizeUri(WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString());

            var broker = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.SilentMode, uri);
            if (broker.ResponseStatus == WebAuthenticationStatus.Success)
            {

            }
            else
            {
                Assert.Fail(broker.ResponseStatus.ToString());
            }
        }
    }
}
