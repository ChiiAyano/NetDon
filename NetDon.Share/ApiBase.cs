using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon
{
    public abstract class ApiBase
    {
        protected abstract string ApiEndpointName { get; }

        protected Uri CreateUriBase(Uri mastodonUri)
        {
            var uri = new Uri(mastodonUri, "/api/v1/" + this.ApiEndpointName);
            return uri;
        }
    }
}
