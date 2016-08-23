using System.Web.Http;
using Swagger;
using WebActivatorEx;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace Swagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);

            SwaggerSpecConfig.Customize(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        protected static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\Swagger.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}