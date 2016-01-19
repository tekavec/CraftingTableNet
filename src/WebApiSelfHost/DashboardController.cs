using System.Net.Http;
using System.Web.Http;

namespace WebApiSelfHost
{
    public class DashboardController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(); 
        }
    }
}