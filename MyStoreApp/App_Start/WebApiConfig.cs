using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using Microsoft.Web.Http.Versioning.Conventions;
using MyStoreApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
namespace MyStoreApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration, versioning and services
            config.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1,0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("ver"));

                //Versioning Conventions example
                cfg.Conventions.Controller<ProductController>()
                .HasApiVersion(1, 0)
                .HasApiVersion(1, 1)
                .Action(m => m.Delete(default(Guid)));

            });
            // Web API routes

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
          config.EnableCors(cors);
        }
    }
}
