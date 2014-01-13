using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace RaphaelPinho.Twitter.Communications
{
    public class Request
    {
        public static T DoGet<T>(Authentication.IOAuthSettings oAuthSettings, string url, dynamic parameters)
        {
            return Do<T>(oAuthSettings, "GET", url, parameters);
        }

        public static T Do<T>(Authentication.IOAuthSettings oAuthSettings, string method, string url, dynamic parameters)
        {
            var authResponse = default(T);
            var request = (HttpWebRequest)WebRequest.Create(GetUrl(method, url, parameters));

            request.Headers.Add("Authorization", oAuthSettings.GetHeader(method, url, parameters));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = method;

            var response = request.GetResponse();

            using (response)
            {
                using (var responseReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = responseReader.ReadToEnd();
                    authResponse = JsonConvert.DeserializeObject<T>(responseText);
                }
            }

            return authResponse;
        }

        private static string GetUrl(string method, string url, dynamic parameters)
        {
            if (method == "GET")
            {
                var queryString = string.Join("&", (parameters as IDictionary<string, object>).OrderBy(x => x.Key).Select(x => x.Key + "=" + Uri.EscapeDataString(x.Value.ToString())).ToArray());

                if (url.IndexOf('?').Equals(-1))
                    return string.Concat(url, "?", queryString);

                return string.Concat(url, "&", queryString);
            }

            return url;
        }
    }
}
