using System.Windows;
using System.Net.Http;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ICURemove.xaml
    /// </summary>
    public partial class IcuRemove
    {
        public IcuRemove()
        {
            InitializeComponent();
        }
        private static readonly HttpClient Client = new HttpClient();
        private async System.Threading.Tasks.Task Remove_ClickAsync()
        {
            var icuId = textBoxIcuId.Text;

            var response = await Client.DeleteAsync("http://localhost:5000/api/IcuDetails/Remove/IcuWards/" + icuId);

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxIcuId.Text = responseString;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            _ = Remove_ClickAsync();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
