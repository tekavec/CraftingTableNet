using System;
using System.Net.Http;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WebApiSelfHost.Tests.Attributes;

namespace WebApiSelfHost.Tests
{
    [TestFixture]
    public class HttpServerShould
    {

        [Test]
        public void return_a_correct_response_for_a_default_get_request()
        {

            using (var httpClient = new HttpClientFactory().Create())
            {
                var response = httpClient.GetAsync("").Result;

                Assert.True(response.IsSuccessStatusCode, "Status code: " + response.StatusCode);
            }
        }

        [Test]
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
                var content = new JsonContent(json);
                var expected = content.ReadAsJsonAsync().Result;
                content.Headers.ContentType.MediaType = "application/json";
                httpClient.PostAsync("", content).Wait();

                var response = httpClient.GetAsync("").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.appUsers);
            }
        }

    }
}