using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace NetDon.Tests
{
    public class BlockTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public BlockTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetBlocksTest()
        {
            var client = GetClient();
            var blocks = await client.GetBlocksAsync();

            output.WriteLine(string.Join("\r\n", blocks));

            blocks = await client.GetBlocksAsync(90, lim: 80);

            output.WriteLine(string.Join("\r\n", blocks));

            blocks = await client.GetBlocksAsync(sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", blocks));

            blocks = await client.GetBlocksAsync(90, 5, 80);

            output.WriteLine(string.Join("\r\n", blocks));
        }
    }
}
