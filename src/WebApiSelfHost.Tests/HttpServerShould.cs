using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Net.Http;
using NUnit.Framework;
using Dapper;
using WebApiSelfHost.Model.User;
using WebApiSelfHost.Tests.Attributes;

namespace WebApiSelfHost.Tests
{
    [TestFixture]
    public class HttpServerShould
    {

        [Test, UseDatabase]
        public void return_a_correct_response_for_a_default_get_request()
        {

            using (var httpClient = new HttpClientFactory().Create())
            {
                var response = httpClient.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code: " + response.StatusCode);
            }
        }

        [Test, UseDatabase]
        public void return_a_correct_response_for_a_post_request()
        {
            using (var httpClient = new HttpClientFactory().Create())
            {
                var entry =
                    new
                    {
                        id = new Guid().ToString(),
                        name = "John Smith",
                        email = "john.smith@noemail.com",
                        active = true,
                        creationTime = new DateTime(2016, 1, 1)
                    };
                var response = httpClient.PostAsJsonAsync("", entry).Result;

                Assert.True(response.IsSuccessStatusCode, "Status code: " + response.StatusCode);
            }
        }

        [Test, UseDatabase]
        public void return_a_posted_entry_after_a_post_request()
        {
            using (var httpClient = new HttpClientFactory().Create())
            {
                var json =
                    new 
                    {
                        id = new Guid(),
                        name = "John Smith",
                        email = "john.smith@noemail.com",
                        active = true,
                        creationTime = new DateTime(2016, 1, 1)
                    };
                var expected = json.ToJObject();
                httpClient.PostAsJsonAsync("", json).Wait();

                var response = httpClient.GetAsync("").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.appUsers);
            }
        }

        [Test, UseDatabase]
        public void return_a_correct_data_from_database_for_a_default_get_request()
        {
            var aGuid = Guid.NewGuid();
            dynamic entry = new ExpandoObject();
            entry.id = aGuid;
            entry.name = "John Smith";
            entry.email = "john.smith@noemail.com";
            entry.active = true;
            entry.creationTime = new DateTime(2016, 1, 1);

            var expected = ((object) entry).ToJObject();

            var appUser = new AppUser
            {
                Id = aGuid,
                Name = "John Smith",
                Email = "john.smith@noemail.com",
                Active = true,
                CreationTime = new DateTime(2016, 1, 1)
            };
            var connectionString = ConfigurationManager.ConnectionStrings["CraftingTable"].ConnectionString;

            using (IDbConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(
                    @"insert AppUsers(Id, Name, Email, Active, CreationTime)
                      values (@Id, @Name, @Email, @Active, @CreationTime)",
                    appUser);
                sqlConnection.Close();
            }

            using (var httpClient = new HttpClientFactory().Create())
            {
                var response = httpClient.GetAsync("").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                foreach (var user in actual.appUsers)
                {
                    Console.WriteLine(user.ToString());
                }
                Assert.Contains(expected, actual.appUsers);
            }
        }
    }
}