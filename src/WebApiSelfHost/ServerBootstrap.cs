using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using WebApiSelfHost.Properties;

namespace WebApiSelfHost
{
    public class ServerBootstrap
    {
        private const string InitialSqlServerCatalog = "Master";

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

        public void CreateDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CraftingTable"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = InitialSqlServerCatalog
            };
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var dbSchema = Resources.CreateDatabase;
                    foreach (var sql in dbSchema.Split(new string[] {"GO"}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DropDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CraftingTable"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = InitialSqlServerCatalog
            };
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var sql = Resources.DropDatabase;
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}