using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class TokenTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public TokenTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetAccessTokenTest()
        {
            var client = GetClient();
            // Please change to your account
            var token = await client.GetAccessTokenAsync("test@test.com", "password");

            output.WriteLine(token);
        }
    }
}
