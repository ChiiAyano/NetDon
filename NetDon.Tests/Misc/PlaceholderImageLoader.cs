using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetDon.Tests.Misc
{
    public static class PlaceholderImageLoader
    {
        private const string placeholditUrl = "http://placehold.it/";
        private static HttpClient http = new HttpClient();

        public static async Task<(byte[] image, string fileName)> GetPlaceholderImageAsync(int width, int height)
        {
            var response = await http.GetAsync(placeholditUrl + width + "x" + height);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            var imageData = await response.Content.ReadAsByteArrayAsync();

            return (imageData, "test.png");
        }
    }
}
