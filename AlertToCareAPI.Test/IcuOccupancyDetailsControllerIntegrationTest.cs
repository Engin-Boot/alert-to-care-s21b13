using AlertToCareAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlertToCareAPI.Test
{
    public class IcuOccupancyDetailsControllerIntegrationTest
    {
        private readonly TestServer _sut;
        private const string Url = "http://localhost:5000/api/IcuOccupancyDetails";

        public IcuOccupancyDetailsControllerIntegrationTest()
        {
            _sut = new TestServer();
        }

        [Fact]
        public async Task GetAllPatientInfo()
        {
            var response = await _sut.Client.GetAsync(Url + "/Patients");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetPatientById()
        {
            var response = await _sut.Client.GetAsync(Url + "/Patients/PID001");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AddNewPatientSuccessfully()
        {
            var newPatient = new PatientDetails
            {
                Address = new PatientAddress
                {
                    City = "Patna",
                    HouseNo = "A370",
                    Pincode = "800004",
                    State = "Bihar",
                    Street = "A.G. Colony"
                },
                PatientId = "PID005",
                PatientName = "Akash",
                Age = 24,
                BedId = "BID5",
                ContactNo = "675566444",
                Email = "akash@akash.com",
                IcuId = "ICU01",
                Vitals = new VitalsCategory
                {
                    PatientId = "PID005",
                    Bpm = 79.2f,
                    RespRate = 118.2f,
                    Spo2 = 66.0f
                }

            };

            var response = await _sut.Client.PostAsync(Url + "/Patients", new StringContent(JsonConvert.SerializeObject(newPatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddNewPatientNotSuccessful()
        {
            var newPatient = new PatientDetails
            {
                Address = new PatientAddress
                {
                    City = "Patna",
                    HouseNo = "A370",
                    Pincode = "800004",
                    State = "Bihar",
                    Street = "A.G. Colony"
                },
                PatientId = "PID005",
                PatientName = "Akash",
                Age = 24,
                BedId = "BID5",
                ContactNo = "675566444",
                Email = "akash@akash.com",
                
                Vitals = new VitalsCategory
                {
                    PatientId = "PID005",
                    Bpm = 79.2f,
                    RespRate = 118.2f,
                    Spo2 = 66.0f
                }

            };

            var response = await _sut.Client.PostAsync(Url + "/Patients", new StringContent(JsonConvert.SerializeObject(newPatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePatientSuccessfully()
        {
            var newPatient = new PatientDetails
            {
                Address = new PatientAddress
                {
                    City = "Patna",
                    HouseNo = "A370",
                    Pincode = "800004",
                    State = "Bihar",
                    Street = "A.G. Colony"
                },
                PatientId = "PID003",
                PatientName = "Akash",
                Age = 24,
                BedId = "BID5",
                ContactNo = "675566444",
                Email = "akash@akash.com",
                IcuId = "ICU01",
                Vitals = new VitalsCategory
                {
                    PatientId = "PID003",
                    Bpm = 79.2f,
                    RespRate = 118.2f,
                    Spo2 = 66.0f
                }

            };

            var response = await _sut.Client.PutAsync(Url + "/Patients/PID003", new StringContent(JsonConvert.SerializeObject(newPatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdatePatientNotSuccessful()
        {
            var newPatient = new PatientDetails
            {
                Address = new PatientAddress
                {
                    City = "Patna",
                    HouseNo = "A370",
                    Pincode = "800004",
                    State = "Bihar",
                    Street = "A.G. Colony"
                },
                PatientId = "PID006",
                PatientName = "Akash",
                Age = 24,
                BedId = "BID5",
                ContactNo = "675566444",
                Email = "akash@akash.com",
                IcuId = "ICU01",
                Vitals = new VitalsCategory
                {
                    PatientId = "PID006",
                    Bpm = 79.2f,
                    RespRate = 118.2f,
                    Spo2 = 66.0f
                }

            };

            var response = await _sut.Client.PutAsync(Url + "/Patients/PID006", new StringContent(JsonConvert.SerializeObject(newPatient), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeletePatientSuccessful()
        {
            var response = await _sut.Client.DeleteAsync(Url + "/Remove/Patients/PID001");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeletePatientNotSuccessful()
        {
            var response = await _sut.Client.DeleteAsync(Url + "/Remove/Patients/PID007");
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}
