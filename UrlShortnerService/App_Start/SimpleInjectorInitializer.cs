
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using UrlShortening.AwsClient;
using UrlShortening.Cache;
using UrlShortening.Contract;
using UrlShortening.Repository;
using UrlShorteningService.Utility;

namespace UrlShorteningService
{
    public static class SimpleInjectorInitializer
    {
        public static Container Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            InitializeContainer(container);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<IClientRepository, AwsRepository>();
            container.Register<IRepository, Repository>();
            container.Register<ICacheManager, InMemoryCacheManager>();
            container.Register<UtilityManager>();
        }
    }
}