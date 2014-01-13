using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaphaelPinho.TestMvcApplication;
using RaphaelPinho.TestMvcApplication.Controllers;
using System.Dynamic;
using RaphaelPinho.Twitter.Services;
using Microsoft.Practices.Unity;
using RaphaelPinho.Twitter.Entities;

namespace RaphaelPinho.TestMvcApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IService service;
        private IUnityContainer container;

        [TestInitialize]
        public void Init()
        {
            container = new UnityContainer();
            ContainerBootstrap.RegisterTypes(container);

            service = container.Resolve<IService>();
        }

        [TestMethod]
        public void Index()
        {
            var controller = new HomeController(service);
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result, "View Result cannot be null");

            var list = result.Model as IEnumerable<Tweet>;

            Assert.IsNotNull(list, "Model list cannot be null");

            var minCreatedAt = DateTime.Now.Date.AddDays(-14);
            var count = list.Where(x => x.CreatedAt < minCreatedAt).Count();

            Assert.AreEqual(list.Where(x=> x.CreatedAt < minCreatedAt).Count(), 0, "List contains items older than 2 weeks");

            var listSorted = list.OrderByDescending(x => x.CreatedAt);

            Assert.IsTrue((list.First() == listSorted.First() && list.Last() == listSorted.Last()), "List is not sorted");
        }
    }
}
