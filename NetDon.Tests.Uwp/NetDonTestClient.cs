namespace NetDon.Tests.Uwp
{
    public class NetDonTestClient
    {
        private string ClientId => "7bf4ca4baf59be4052a136fe4cf84ee43dfa41396cff8f33f877613730cc80a6";
        private string ClientSecret => "3c8251f0f3d2e6b3e497c0bafb8da2aa0cc4c4d334534828e73ed93ed8187401";

        protected Client GetClient()
        {
            return new Client("https://m6n.onsen.tech/", this.ClientId, this.ClientSecret);
        }
    }
}
