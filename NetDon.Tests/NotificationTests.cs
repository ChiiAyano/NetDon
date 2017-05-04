using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;


namespace NetDon.Tests
{
    public class NotificationTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public NotificationTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task GetNotificationsTest()
        {
            var client = GetClient();
            var notifications = await client.GetNotificationsAsync();

            output.WriteLine(string.Join("\r\n", notifications));

            notifications = await client.GetNotificationsAsync(90, lim: 80);

            output.WriteLine(string.Join("\r\n", notifications));

            notifications = await client.GetNotificationsAsync(sinceId: 5, lim: 80);

            output.WriteLine(string.Join("\r\n", notifications));

            notifications = await client.GetNotificationsAsync(90, 5, 80);

            output.WriteLine(string.Join("\r\n", notifications));
        }

        [Fact]
        public async Task GetNotificationTest()
        {
            var client = GetClient();
            var notification = await client.GetNotificationsAsync(62);

            output.WriteLine(notification.ToString());
        }
    }
}
