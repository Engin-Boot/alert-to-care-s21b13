using System.Windows;
using System.Net.Http;


namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for PatientRemove.xaml
    /// </summary>
    public partial class PatientRemove
    {
        private static readonly HttpClient Client = new HttpClient();
        public PatientRemove()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task Remove_ClickAsync()
        {
            var patientId = textBoxPatientId.Text;

            var response = await Client.DeleteAsync("http://localhost:5000/api/IcuOccupancyDetails/Remove/Patients/" + patientId);

            var responseString = await response.Content.ReadAsStringAsync();

            textBoxPatientId.Text = responseString;
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
