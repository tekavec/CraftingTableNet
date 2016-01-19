using NUnit.Framework;

namespace WebApiSelfHost.Tests
{
    [TestFixture]
    public class HttpServerShould
    {

        [Test]
        public void return_correct_response_for_a_default_get_request()
        {

            using (var httpClient = new HttpClientFactory().Create())
            {
                var response = httpClient.GetAsync("").Result;

                Assert.IsTrue(response.IsSuccessStatusCode, "Status code: " + response.StatusCode);
            }
        }

    }
}