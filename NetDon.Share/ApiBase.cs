using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon
{
    public abstract class ApiBase
    {
        protected Uri CreateUriBase(Uri mastodonUri, string endpoint)
        {
            var uri = new Uri(mastodonUri, "/api/v1/" + endpoint);
            return uri;
        }
    }
}
