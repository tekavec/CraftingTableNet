using System.Web.Http;
using Newtonsoft.Json.Serialization;

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

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}