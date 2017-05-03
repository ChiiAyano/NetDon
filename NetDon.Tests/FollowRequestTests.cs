using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace NetDon.Tests
{
    public class FollowRequestTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public FollowRequestTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetFavoritesTest()
        {
            var client = GetClient();
            var followRequests = await client.GetFollowRequestsAsync();

            output.WriteLine(string.Join("\r\n", followRequests));

            followRequests = await client.GetFollowRequestsAsync(90, lim: 80);

            output.WriteLine(string.Join("\r\n", followRequests));

            followRequests = await client.GetFollowRequestsAsync(sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", followRequests));

            followRequests = await client.GetFollowRequestsAsync(90, 5, 80);

            output.WriteLine(string.Join("\r\n", followRequests));
        }
    }
}
