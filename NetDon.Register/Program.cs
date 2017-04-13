using System;

namespace NetDon.Register
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri mastodonUri;
            var appName = string.Empty;
            var redirect = string.Empty;
            var scopes = string.Empty;
            var website = string.Empty;

            while (true)
            {
                Console.Write("Mastodon URI: ");
                var uriStr = Console.ReadLine();

                if (Uri.TryCreate(uriStr, UriKind.Absolute, out mastodonUri))
                {
                    break;
                }
            }

            while (string.IsNullOrWhiteSpace(appName))
            {
                Console.Write("App name: ");
                appName = Console.ReadLine();
            }

            Console.Write("Redirect Uri (optional): ");
            redirect = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(redirect)) redirect = Apps.DefaultRedirectUri;

            Console.Write("Scope (space-separated, \"read\", \"write\", \"follow\": ");
            scopes = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(scopes)) scopes = "read";

            Console.Write("Website Uri (optional): ");
            website = Console.ReadLine();


            var apps = new Apps();
            apps.RegisterAppAsync(mastodonUri, appName, redirect, scopes, website).Wait();
        }
    }
}