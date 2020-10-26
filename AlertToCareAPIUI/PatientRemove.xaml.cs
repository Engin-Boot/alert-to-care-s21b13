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
    /// Interaction logic for PatientRemove.xaml
    /// </summary>
    public partial class PatientRemove : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public PatientRemove()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task Remove_ClickAsync(object sender, RoutedEventArgs e)
        {
            var PatientId = textBoxPatientId.Text;

            var response = await client.DeleteAsync("http://localhost:5000/api/IcuOccupancyDetails/Remove/Patients/" + PatientId);

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxPatientId.Text = responseString;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var result = Remove_ClickAsync(sender, e);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
