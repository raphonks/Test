using Microsoft.Practices.Unity;

namespace RaphaelPinho.Twitter.Services
{
    public class ContainerBootstrap
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IService, Service>()
                .RegisterType<Authentication.IOAuthSettings, Authentication.OAuthSettings>();
        }
    }
}
