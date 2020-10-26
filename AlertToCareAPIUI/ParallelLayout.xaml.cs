using AlertToCareAPI.Database;
using AlertToCareAPI.Models;
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
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ParallelLayout.xaml
    /// </summary>
    ///
    public partial class ParallelLayout : Window
    {
        public ParallelLayout()
        {
            InitializeComponent();
        }

        public List<BedDetails> FindBeds(string responseBody)
        {
            
            var myICUBedDetailsList = JsonConvert.DeserializeObject<ICUBedDetails>(responseBody);


            var myBedDetails = myICUBedDetailsList.Beds;

            var result = new List<BedDetails>();

                foreach (var bed in myBedDetails)
                {
                    result.Add(bed);
                }
            
            return result;
        }

        public async System.Threading.Tasks.Task GetBeds_ClickAsync(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/IcuDetails/IcuWards/ICU01");
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var myICUBedDetailsList = JsonConvert.DeserializeObject<ICUBedDetails>(responseBody);

            int bedValue = myICUBedDetailsList.BedsCount;

            MakeRectangle(bedValue,responseBody);

        }

        public void MakeRectangle(int bedValue,string responseBody)
        {
            var myBeds = FindBeds(responseBody);
          
            int width = 75;
            int height = 75;
            int top = 20;
            int left = 20;
            int tbTop = 100;

            for (int i = 0; i < bedValue/2; i++)
            {
                Rectangle rec = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.AliceBlue,
                    Stroke = Brushes.LightPink,
                    StrokeThickness = 2,
                    
                };

                TextBlock tb = new TextBlock()
                {
                    Width = 75,
                    Height = 80,
                    Text = myBeds[i].Status.ToString() +" "+ myBeds[i].BedId,
                    
                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, tbTop);
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(rec, top);
                Canvas.SetLeft(rec, left);
                left = left + 100;
            }
            top = top + 150;
            tbTop = tbTop + 150;
            left = 20;

            for (int i = bedValue/2; i < bedValue; i++)
            {
                // Create the rectangle
                Rectangle rec = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.AliceBlue,
                    Stroke = Brushes.LightPink,
                    StrokeThickness = 2,
                };

                TextBlock tb = new TextBlock()
                {
                    Width = 75,
                    Height = 80,
                    Text = myBeds[i].Status.ToString() + " " + myBeds[i].BedId,

                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, tbTop);
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(rec, top);
                Canvas.SetLeft(rec, left);
                left = left + 100;
            }
           

        }

        private void GetBeds_Click(object sender, RoutedEventArgs e)
        {
            var result = GetBeds_ClickAsync(sender, e);

        }

        public async System.Threading.Tasks.Task GetAlert_ClickAsync(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/PatientMonitoring");
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            MessageBox.Show(responseBody);
        }

        private void GetAlert_Click(object sender, RoutedEventArgs e)
        {
            var result = GetAlert_ClickAsync(sender, e);
        }
        private void UndoAlert_Click(object sender, RoutedEventArgs e)
        {
            var result = GetAlert_ClickAsync(sender, e);
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new PatientDetailsWindow();
            newWind.Show();
        }

        private void RemovePatient_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new PatientRemove();
            newWind.Show();
        }
    }
}
