using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Swagger.App_Start;

[assembly: OwinStartup(typeof(Swagger.Startup))]
namespace Swagger
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            HttpConfiguration config = new HttpConfiguration();
            
            WebApiConfig.Register(config);

            Swashbuckle.Bootstrapper.Init(config);

            app.UseWebApi(config);

        }
    }
}