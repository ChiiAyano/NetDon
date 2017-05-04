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
        public async Task GetCurrentAccountTest()
        {
            var client = GetClient();
            var account = await client.GetCurrentUserAsync();

            output.WriteLine(account.ToString());
        }

        [Fact]
        public async Task GetAccountTest()
        {
            var client = GetClient();
            var account = await client.GetUserAsync(2);

            output.WriteLine(account.ToString());
        }

        [Fact]
        public async Task GetFollowersTest()
        {
            var client = GetClient();
            var followers = await client.GetUserFollowers(1);

            output.WriteLine(string.Join("\r\n", followers));

            followers = await client.GetUserFollowers(1, 90, limit: 80);

            output.WriteLine(string.Join("\r\n", followers));

            followers = await client.GetUserFollowers(1, sinceId: 5, limit: 80);

            output.WriteLine(string.Join("\r\n", followers));

            followers = await client.GetUserFollowers(1, 90, 5, 80);

            output.WriteLine(string.Join("\r\n", followers));
        }

        [Fact]
        public async Task GetFollowingTest()
        {
            var client = GetClient();
            var following = await client.GetUserFollowing(1);

            output.WriteLine(string.Join("\r\n", following));

            following = await client.GetUserFollowing(1, 90, limit: 80);

            output.WriteLine(string.Join("\r\n", following));

            following = await client.GetUserFollowing(1, sinceId: 5, limit: 80);

            output.WriteLine(string.Join("\r\n", following));

            following = await client.GetUserFollowing(1, 90, 5, 80);

            output.WriteLine(string.Join("\r\n", following));
        }

        [Fact]
        public async Task GetRelationshipTest()
        {
            var client = GetClient();
            var relationship = await client.GetRelationShipsAsync(19);

            output.WriteLine(relationship.ToString());
        }

        [Fact]
        public async Task GetRelationshipsTest()
        {
            var client = GetClient();
            var relationships = await client.GetRelationShipsAsync(new long[] { 1, 19 });

            output.WriteLine(string.Join("\r\n", relationships));
        }

        [Fact]
        public async Task SearchTest()
        {
            var client = GetClient();
            var search = await client.SearchAccountAsync("@ayn@m6n.onsen.tech");

            output.WriteLine(string.Join("\r\n", search));
        }
    }
}
