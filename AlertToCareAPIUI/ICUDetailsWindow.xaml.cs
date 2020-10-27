using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Net.Http;
using AlertToCareAPI.Models;
using Newtonsoft.Json;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ICUDetailsWindow.xaml
    /// </summary>
    public partial class ICUDetailsWindow
    {
        private static readonly HttpClient Client = new HttpClient();
        public ICUDetailsWindow()
        {
            InitializeComponent();
        }
        private async System.Threading.Tasks.Task AddIcuDetails_ClickAsync()
        {
            var newBedDetails = new BedDetails
            {
                BedId = textBoxBedId.Text,
                IcuId = textBoxIcuId.Text,
                Status = bool.Parse(textBoxStatus.Text)
            };

            var bedDetailsList = new List<BedDetails>
            {
                newBedDetails
            };

            var newIcuDetails = new ICUBedDetails
            {
                IcuId = textBoxIcuId.Text,
                LayoutId = textBoxLayoutId.Text,
                BedsCount = int.Parse(textBoxBedsCount.Text),
                Beds = bedDetailsList
            };

            var response = await Client.PostAsync("http://localhost:5000/api/IcuDetails/IcuWards", new StringContent(JsonConvert.SerializeObject(newIcuDetails), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxBedId.Text= responseString;
            
        }

        private void AddIcuDetails_Click(object sender, RoutedEventArgs e)
        {
            _ = AddIcuDetails_ClickAsync();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
