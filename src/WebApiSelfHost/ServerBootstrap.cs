using System.Web.Http;

namespace WebApiSelfHost
{
    public class ServerBootstrap
    {
        public void Configure(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "{controller}/{id}",
                defaults: new
                {
                    controller = "Dashboard",
                    id = RouteParameter.Optional                    
                });
        }
    }
}