using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using UrlShorteningService;

namespace UrlShortnerService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            var container = SimpleInjectorInitializer.Initialize();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
