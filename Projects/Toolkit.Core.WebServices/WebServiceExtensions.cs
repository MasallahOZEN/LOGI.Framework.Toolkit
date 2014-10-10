using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;

namespace LOGI.Framework.Toolkit.Core.WebServices
{
    public static class WebServiceExtensions
    {
        public static void SetCredential(this SoapHttpClientProtocol clientProtocol, string userName, string password, string url, string authenticationType = "Basic")
        {
            var nc = new NetworkCredential(userName, password);
            var uri = new Uri(url);

            ICredentials credentials = nc.GetCredential(uri, authenticationType);
            clientProtocol.Credentials = credentials;
            clientProtocol.PreAuthenticate = true;

            clientProtocol.Url = url;
        }
    }
}
