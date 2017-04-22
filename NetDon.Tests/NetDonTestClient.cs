namespace NetDon.Tests
{
    public class NetDonTestClient
    {
        // Please Set to Your Token
        private string AccessToken => "";

        protected Client GetClient()
        {
            return new Client("https://m.moriya.faith", this.AccessToken);
        }
    }
}
