using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlertToCareAPI.Models;
using Newtonsoft.Json;
using Xunit;

namespace AlertToCareAPI.Test
{
    public class IcuDetailsIntegrationTest
    {
        private readonly TestServer _sut;
        private const string Url = "http://localhost:5000/api/IcuDetails";

        public IcuDetailsIntegrationTest()
        {
            _sut = new TestServer();
        }

        private static ICUBedDetails PatientInfo(string icuId)
        {
            return new ICUBedDetails
            {
                BedsCount = 3,
                Beds = new List<BedDetails>
                {
                    new BedDetails
                    {
                        BedId = "BID50",
                        IcuId = icuId,
                        Status = false
                    },
                    new BedDetails
                    {
                        BedId = "BID51",
                        IcuId = icuId,
                        Status = false
                    },
                    new BedDetails
                    {
                        BedId = "BID52",
                        IcuId = icuId,
                        Status = false
                    }
                },
                IcuId = icuId,
                LayoutId = "LID04"

            };
        }

        
        [Fact]
        public async Task GetIcuWardsById()
        {
            var response = await _sut.Client.GetAsync(Url + "/IcuWards/ICU01");
            var returnString = await response.Content.ReadAsStringAsync();
            Assert.Contains("ICU01", returnString);

        }

        [Fact]
        public async Task AddIcuSuccessfully()
        {
            
            var response = await _sut.Client.PostAsync(Url + "/IcuWards",
                new StringContent(JsonConvert.SerializeObject(PatientInfo("ICU04")), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode==HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetIcu()
        {
            var response = await _sut.Client.GetAsync(Url + "/IcuWards");
            Assert.True(response.StatusCode==HttpStatusCode.OK);
        }

        [Fact]
        public async Task IcuNotAddedSuccessfully()
        {
            var newIcu = new ICUBedDetails
            {
                BedsCount = 3,
                Beds = new List<BedDetails>
                {
                    new BedDetails
                    {
                        BedId = "BID50",
                        IcuId = "ICU04",
                        Status = false
                    },
                    new BedDetails
                    {
                        BedId = "BID51",
                        IcuId = "ICU04",
                        Status = false
                    },
                    new BedDetails
                    {
                        BedId = "BID52",
                        IcuId = "ICU04",
                        Status = false
                    }
                }
                

            };
            var response = await _sut.Client.PostAsync(Url + "/IcuWards",
                new StringContent(JsonConvert.SerializeObject(newIcu), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateIcuSuccessfully()
        {
            
            var response = await _sut.Client.PutAsync(Url + "/IcuWards/ICU03",
                new StringContent(JsonConvert.SerializeObject(PatientInfo("ICU03")), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateIcuNotSuccessfully()
        {
            
            var response = await _sut.Client.PutAsync(Url + "/IcuWards/ICU05",
                new StringContent(JsonConvert.SerializeObject(PatientInfo("ICU03")), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteIcuWards()
        {
            var response = await _sut.Client.DeleteAsync(Url + "/Remove/IcuWards/ICU03");
            Assert.True(response.StatusCode==HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteIcuWardsNotSuccessful()
        {
            var response = await _sut.Client.DeleteAsync(Url + "/Remove/IcuWards/ICU04");
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }

    
}
