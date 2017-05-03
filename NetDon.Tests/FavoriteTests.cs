using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace NetDon.Tests
{
    public class FavoriteTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public FavoriteTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetFavoritesTest()
        {
            var client = GetClient();
            var favorites = await client.GetFavoritesAsync();

            output.WriteLine(string.Join("\r\n", favorites));

            favorites = await client.GetFavoritesAsync(90, lim: 80);

            output.WriteLine(string.Join("\r\n", favorites));

            favorites = await client.GetFavoritesAsync(sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", favorites));

            favorites = await client.GetFavoritesAsync(90, 5, 80);

            output.WriteLine(string.Join("\r\n", favorites));
        }
    }
}
