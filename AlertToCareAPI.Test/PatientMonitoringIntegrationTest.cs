using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AlertToCareAPI.Test
{
    public class PatientMonitoringIntegrationTest
    {
        private readonly TestServer _sut;
        private const string Url = "http://localhost:5000/api/PatientMonitoring";

        public PatientMonitoringIntegrationTest()
        {
            _sut = new TestServer();
        }


        [Fact]
        public async Task GetVitalsSuccessfully()
        {
            var response = await _sut.Client.GetAsync(Url);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
