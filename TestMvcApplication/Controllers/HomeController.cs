using RaphaelPinho.Twitter.Entities;
using RaphaelPinho.Twitter.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace RaphaelPinho.TestMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        private IService service;

        public HomeController(IService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var tweets = GetTweets();
            ViewBag.Message = "Tweets from @pay_by_phone, @PayByPhone and @PayByPhone_UK";

            return View(tweets);
        }

        public IEnumerable<Tweet> GetTweets()
        {
            var minCreatedAt = DateTime.Now.Date.AddDays(-14);

            var stTweets = Search("pay_by_phone");
            var ndTweets = Search("PayByPhone");
            var rdTweets = Search("PayByPhone_UK");

            return stTweets
                .Union(ndTweets)
                .Union(rdTweets)
                .Where(x => x.CreatedAt >= minCreatedAt)
                .OrderByDescending(x => x.CreatedAt);
        }

        public List<Tweet> Search(string from)
        {
            dynamic parameters = new ExpandoObject();
            parameters.q = string.Format("from:{0}", from);
            parameters.count = 100;

            return service.Search(parameters);
        }
    }
}
