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

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ICURemove.xaml
    /// </summary>
    public partial class ICURemove : Window
    {
        public ICURemove()
        {
            InitializeComponent();
        }
        private static readonly HttpClient client = new HttpClient();
        private async System.Threading.Tasks.Task Remove_ClickAsync(object sender, RoutedEventArgs e)
        {
            var IcuId = textBoxIcuId.Text;

            var response = await client.DeleteAsync("http://localhost:5000/api/IcuDetails/Remove/IcuWards/" + IcuId);

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxIcuId.Text = responseString;
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
