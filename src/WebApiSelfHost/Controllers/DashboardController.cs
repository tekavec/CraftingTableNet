using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using Dapper.Contrib.Extensions;
using WebApiSelfHost.Model.User;

namespace WebApiSelfHost.Controllers
{
    public class DashboardController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CraftingTable"].ConnectionString;
            IDbConnection sqlConnection = new SqlConnection(connectionString);

            //var appUsers1 = sqlConnection.Query<AppUser>("Select * from AppUsers").ToArray<AppUser>();
            var appUsers = sqlConnection.GetAll<AppUser>().ToArray<AppUser>();

            sqlConnection.Close();

            return Request.CreateResponse(
                HttpStatusCode.OK,
                new AppUsersModel {AppUsers = appUsers});
        }

        public HttpResponseMessage Post(AppUser model)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CraftingTable"].ConnectionString;
            using (IDbConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(
                    @"insert AppUsers(Id, Name, Email, Active, CreationTime)
                      values (@Id, @Name, @Email, @Active, @CreationTime)",
                    model);
                sqlConnection.Close();
            }
            return Request.CreateResponse();
        }

    }
}