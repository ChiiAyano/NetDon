using NetDon.Tests.Misc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NetDon.Tests
{
    public class MediaTests : NetDonTestClient
    {
        private readonly ITestOutputHelper output;

        public MediaTests(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public async Task FileUploadTest1()
        {
            var imageData = await PlaceholderImageLoader.GetPlaceholderImageAsync(350, 150);

            var client = GetClient();
            var attachment = await client.PostMediaAsync(imageData.image, imageData.fileName);

            this.output.WriteLine(attachment.ToString());
        }
    }
}
