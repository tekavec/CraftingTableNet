using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSelfHost.Model.Users;

namespace WebApiSelfHost.Controllers
{
    public class DashboardController : ApiController
    {
        private static readonly IList<AppUserEditModel> AppUserEditModels = new List<AppUserEditModel>();

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new AppUserModel {AppUsers = AppUserEditModels.ToArray()});
        }

        public HttpResponseMessage Post(AppUserEditModel model)
        {
            AppUserEditModels.Add(model);
            return Request.CreateResponse();
        }

    }
}