using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;
using WebApp;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Project Manager");
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}