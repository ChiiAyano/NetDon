using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class ReportTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public ReportTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetReportsTest()
        {
            var client = GetClient();
            var reports = await client.GetReportsAsync();

            this.output.WriteLine(string.Join("\r\n", reports));
        }
    }
}
