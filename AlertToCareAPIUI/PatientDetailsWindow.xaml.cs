using System.Text;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using AlertToCareAPI.Models;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for PatientDetailsWindow.xaml
    /// </summary>
    public partial class PatientDetailsWindow
    {
        private static readonly HttpClient Client = new HttpClient();
        public PatientDetailsWindow()
        {
            InitializeComponent();
        }
        private async System.Threading.Tasks.Task AddPatientDetails_ClickAsync()
        {
            var newAddress = new PatientAddress()
            {
                HouseNo = textBoxHouseNo.Text,
                Street = textBoxStreet.Text,
                City = textBoxCity.Text,
                State = textBoxState.Text,
                Pincode = textBoxPincode.Text
            };

            var newVitals = new VitalsCategory()
            {
                PatientId = textBoxPatientId.Text,
                Spo2 = float.Parse(textBoxSpo2.Text),
                Bpm = float.Parse(textBoxBpm.Text),
                RespRate = float.Parse(textBoxResp.Text)

            };

            var newPatient = new PatientDetails()
            {
                PatientId = textBoxPatientId.Text,
                PatientName = textBoxPatientName.Text,
                Age = int.Parse(textBoxAge.Text),
                ContactNo = textBoxContactNo.Text,
                BedId = textBoxBedId.Text,
                IcuId = textBoxIcuId.Text,
                Email = textBoxEmail.Text,
                Address = newAddress,
                Vitals = newVitals
            };

            var response = await Client.PostAsync("http://localhost:5000/api/IcuOccupancyDetails/Patients", new StringContent(JsonConvert.SerializeObject(newPatient), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxPatientId.Text = responseString;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            _ = AddPatientDetails_ClickAsync();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
