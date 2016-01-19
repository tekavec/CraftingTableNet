using System;
using System.Net.Http;
using System.Web.Http.SelfHost;

namespace WebApiSelfHost.Tests
{
    public class HttpClientFactory
    {
        public HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:12345");
            var httpSelfHostConfiguration = new HttpSelfHostConfiguration(baseAddress);
            new ServerBootstrap().Configure(httpSelfHostConfiguration);
            var server = new HttpSelfHostServer(httpSelfHostConfiguration);
            var httpClient = new HttpClient(server);
            try
            {
                httpClient.BaseAddress = baseAddress;
                return httpClient;
            }
            catch 
            {
                httpClient.Dispose();
                throw;
            }
        }
    }
}