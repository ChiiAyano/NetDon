using System;
using System.IO;
using System.Reflection;
using System.Text;

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

            Console.Write("Scope (space-separated: \"read\", \"write\", \"follow\")(default: read): ");
            scopes = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(scopes)) scopes = "read";

            Console.Write("Website Uri (optional): ");
            website = Console.ReadLine();


            var apps = new Apps(mastodonUri);
            var result = apps.RegisterAppAsync(appName, redirect, scopes, website).Result;

            // ファイルに保存
            var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var filePath = Path.Combine(assemblyPath, "output.txt");
            File.WriteAllText(filePath, result.ToString());

            Console.WriteLine("File written: " + filePath);
            Console.Write("Please any key to exit:");

            Console.ReadKey();
        }
    }
}