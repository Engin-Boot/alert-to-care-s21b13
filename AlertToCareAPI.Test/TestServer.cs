using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace AlertToCareAPI.Test
{
    public class TestServer
    {
        public HttpClient Client { get; private set; }
        private Microsoft.AspNetCore.TestHost.TestServer _server;

        public TestServer()
        {
            SetupClient();
        }

       private void SetupClient()
        {
            _server = new Microsoft.AspNetCore.TestHost.TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
