using Microsoft.Practices.Unity;
using RaphaelPinho.TestMvcApplication.Controllers;
using RaphaelPinho.Twitter.Services;
using System;

namespace RaphaelPinho.TestMvcApplication.App_Start
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static Support.UnityDependencyResolver GetDependencyResolver()
        {
            return new Support.UnityDependencyResolver(container.Value);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ContainerBootstrap.RegisterTypes(container);
            container.RegisterType<HomeController>();
        }
    }
}