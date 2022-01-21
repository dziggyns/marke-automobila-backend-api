using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using Unity.Lifetime;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;
using ZavrsniMojaPriprema1.Repository;
using ZavrsniMojaPriprema1.Resolver;

namespace ZavrsniMojaPriprema1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // CORS
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Tracing
            config.EnableSystemDiagnosticsTracing();

            // Unity
            var container = new UnityContainer();
            container.RegisterType<IMarkaRepository, MarkaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAutomobilRepository, AutomobilRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStatistikaRepository, StatistikaRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Marka, MarkaDTO>();
                cfg.CreateMap<Automobil, AutomobilDTO>();
                //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // ako želimo eksplicitno zadati mapiranje
            });
        }
    }
}
