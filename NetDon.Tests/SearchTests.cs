using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class SearchTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public SearchTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task SearchTest()
        {
            var client = GetClient();
            var result = await client.SearchAsync("@ayn", false);
            this.output.WriteLine(result.ToString());

            result = await client.SearchAsync("ぬるぽ", false);
            this.output.WriteLine(result.ToString());

            result = await client.SearchAsync("#はい", false);
            this.output.WriteLine(result.ToString());
        }
    }
}
