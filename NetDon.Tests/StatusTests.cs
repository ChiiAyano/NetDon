using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class StatusTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public StatusTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetStatusTest()
        {
            var client = GetClient();
            var status = await client.GetStatusAsync(2998);

            this.output.WriteLine(status.ToString());
        }

        [Fact]
        public async Task GetContextTest()
        {
            var client = GetClient();
            var context = await client.GetStatusContextAsync(2998);

            this.output.WriteLine(context.ToString());
        }

        [Fact]
        public async Task GetCardTest()
        {
            var client = GetClient();
            var card = await client.GetStatusCardAsync(2998);

            this.output.WriteLine(card.ToString());
        }

        [Fact]
        public async Task GetRebloggedByTest()
        {
            var id = 3362;
            var client = GetClient();
            var reblogged = await client.GetRebloggedByAsync(id);

            output.WriteLine(string.Join("\r\n", reblogged));

            reblogged = await client.GetRebloggedByAsync(id, 90, lim: 80);

            output.WriteLine(string.Join("\r\n", reblogged));

            reblogged = await client.GetRebloggedByAsync(id, sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", reblogged));

            reblogged = await client.GetRebloggedByAsync(id, 90, 5, 80);

            output.WriteLine(string.Join("\r\n", reblogged));
        }

        [Fact]
        public async Task GetFavoritedByTest()
        {
            var id = 3362;
            var client = GetClient();
            var favorited = await client.GetFavoritedByAsync(id);

            output.WriteLine(string.Join("\r\n", favorited));

            favorited = await client.GetFavoritedByAsync(id, 90, lim: 80);

            output.WriteLine(string.Join("\r\n", favorited));

            favorited = await client.GetFavoritedByAsync(id, sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", favorited));

            favorited = await client.GetFavoritedByAsync(id, 90, 5, 80);

            output.WriteLine(string.Join("\r\n", favorited));
        }
    }
}
