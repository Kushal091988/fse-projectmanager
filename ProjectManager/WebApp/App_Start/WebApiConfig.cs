using AutoMapper;
using BusinessTier.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Intefaces;
using Newtonsoft.Json.Serialization;
using ProjectManager.Api.Extension;
using ProjectManager.Api.Extension.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Unity;
using Unity.Lifetime;
using WebApp.CustomHandler;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);

            // Set JSON formatter as default one and remove XmlFormatter
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            //unity configuration
            var container = new UnityContainer();
            container.RegisterType<IUserFacade, UserFacade>(new HierarchicalLifetimeManager());
            container.RegisterType<IProjectFacade, ProjectFacade>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserFacade, IUserFacade>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //Registering GlobalExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}