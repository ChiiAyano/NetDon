using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class AccountTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public AccountTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetAccountTest()
        {
            var client = GetClient();
            var account = await client.GetCurrentUserAsync();

            output.WriteLine(account);
        }

        [Fact]
        public async Task GetHomeTimelineTest()
        {
            var client = GetClient();
            var timelines = await client.GetHomeTimelineAsync();

            output.WriteLine(timelines);
        }
    }
}
