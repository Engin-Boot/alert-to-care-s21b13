using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using AlertToCareAPI;
using AlertToCareAPI.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ICUDetailsWindow.xaml
    /// </summary>
    public partial class ICUDetailsWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public ICUDetailsWindow()
        {
            InitializeComponent();
        }
        private async System.Threading.Tasks.Task AddIcuDetails_ClickAsync(object sender, RoutedEventArgs e)
        {
            var newBedDetails = new BedDetails
            {
                BedId = textBoxBedId.Text,
                IcuId = textBoxIcuId.Text,
                Status = bool.Parse(textBoxStatus.Text),
            };

            List<BedDetails> bedDetailsList = new List<BedDetails>();
            bedDetailsList.Add(newBedDetails);

            var newIcuDetails = new ICUBedDetails
            {
                IcuId = textBoxIcuId.Text,
                LayoutId = textBoxLayoutId.Text,
                BedsCount = int.Parse(textBoxBedsCount.Text),
                Beds = bedDetailsList,
            };

            var response = await client.PostAsync("http://localhost:5000/api/IcuDetails/IcuWards", new StringContent(JsonConvert.SerializeObject(newIcuDetails), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxBedId.Text= responseString;
            
        }

        private void AddIcuDetails_Click(object sender, RoutedEventArgs e)
        {
            var result = AddIcuDetails_ClickAsync(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
