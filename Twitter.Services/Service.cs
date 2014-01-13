using RaphaelPinho.Twitter.Authentication;
using RaphaelPinho.Twitter.Communications;
using System.Collections.Generic;

namespace RaphaelPinho.Twitter.Services
{
    public class Service : IService
    {
        IOAuthSettings oAuthSettings;

        public Service(IOAuthSettings oAuthSettings)
        {
            this.oAuthSettings = oAuthSettings;
        }

        public List<Entities.Tweet> Search(dynamic parameters)
        {
            var url = "https://api.twitter.com/1.1/search/tweets.json";
            var response = Request.DoGet<SearchResponse>(oAuthSettings, url, parameters);
            return response.Statuses;
        }
    }
}
