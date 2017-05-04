using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class TimelineTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public TimelineTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetHomeTimelineTest()
        {
            var client = GetClient();
            var timelines = await client.GetHomeTimelineAsync();

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(true);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(false, 90, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(false, sinceId: 5, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(false, 90, 5, 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(true, 90, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(true, sinceId: 5, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetHomeTimelineAsync(true, 90, 5, 40);

            this.output.WriteLine(string.Join("\r\n", timelines));
        }

        [Fact]
        public async Task GetPublicTimelineTest()
        {
            var client = GetClient();
            var timelines = await client.GetPublicTimelineAsync();

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(true);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(false, 90, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(false, sinceId: 5, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(false, 90, 5, 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(true, 90, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(true, sinceId: 5, lim: 40);

            this.output.WriteLine(string.Join("\r\n", timelines));

            timelines = await client.GetPublicTimelineAsync(true, 90, 5, 40);

            this.output.WriteLine(string.Join("\r\n", timelines));
        }
    }
}
