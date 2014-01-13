using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RaphaelPinho.Twitter.Authentication
{
    public class OAuthSettings : IOAuthSettings
    {
        public readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public OAuthSettings()
        {
            ConsumerKey = ConfigurationManager.AppSettings["OAuthConsumerKey"];
            ConsumerSecret = ConfigurationManager.AppSettings["OAuthConsumerSecret"];
            AccessToken = ConfigurationManager.AppSettings["OAuthAccessToken"];
            AccessTokenSecret = ConfigurationManager.AppSettings["OAuthAccessTokenSecret"];
            SignatureMethod = ConfigurationManager.AppSettings["OAuthSignatureMethod"];
            Version = ConfigurationManager.AppSettings["OAuthVersion"];
            RequestTokenUrl = ConfigurationManager.AppSettings["OAuthRequestTokenUrl"];
        }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string SignatureMethod { get; set; }
        public string Version { get; set; }
        public string RequestTokenUrl { get; set; }

        public string GetHeader(string method, string url, dynamic parameters)
        {
            var nonce = Guid.NewGuid().ToString("N");
            var signature = GenerateSignature(nonce, method, url, parameters);

            return string.Format(
                "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\""+
                ", oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", oauth_version=\"{6}\"", 
                Uri.EscapeDataString(ConsumerKey),
                nonce,
                Uri.EscapeDataString(signature),
                SignatureMethod,
                GetTimestamp(),
                Uri.EscapeDataString(AccessToken),
                Version
                );
        }

        private string GenerateSignature(string nonce, string method, string url, dynamic parameters) 
        {
            var allParameters = parameters as IDictionary<string, object>;
            allParameters.Add("oauth_consumer_key", ConsumerKey);
            allParameters.Add("oauth_nonce", nonce);
            allParameters.Add("oauth_signature_method", SignatureMethod);
            allParameters.Add("oauth_timestamp", GetTimestamp());
            allParameters.Add("oauth_token", AccessToken);
            allParameters.Add("oauth_version", Version);

            var baseString = string.Join("&", allParameters.OrderBy(x => x.Key).Select(x => x.Key + "=" + Uri.EscapeDataString(x.Value.ToString())).ToArray());

            baseString = string.Concat(method, "&", Uri.EscapeDataString(url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(ConsumerSecret), "&", Uri.EscapeDataString(AccessTokenSecret));

            var signature = string.Empty;
            using (var hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            return signature;
        }

        private string GetTimestamp()
        {
            return (DateTime.UtcNow - UnixEpoch).TotalSeconds.ToString("0"); 
        }
    }
}
