using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RaphaelPinho.Twitter.Authentication
{
    public interface IOAuthSettings
    {
        string ConsumerKey { get; set; }
        string ConsumerSecret { get; set; }
        string AccessToken { get; set; }
        string AccessTokenSecret { get; set; }
        string SignatureMethod { get; set; }
        string Version { get; set; }
        string RequestTokenUrl { get; set; }

        string GetHeader(string method, string url, dynamic parameters);
    }
}
