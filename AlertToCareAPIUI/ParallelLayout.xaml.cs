using AlertToCareAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for ParallelLayout.xaml
    /// </summary>
    ///
    public partial class ParallelLayout
    {
        public ParallelLayout()
        {
            InitializeComponent();
        }

        public List<BedDetails> FindBeds(string responseBody)
        {
            return JsonConvert.DeserializeObject<ICUBedDetails>(responseBody).Beds.ToList();
        }

        public async System.Threading.Tasks.Task GetBeds_ClickAsync(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/api/IcuDetails/IcuWards/ICU01");
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var myIcuBedDetailsList = JsonConvert.DeserializeObject<ICUBedDetails>(responseBody);
            var bedValue = myIcuBedDetailsList.BedsCount;
            MakeRectangle(bedValue,responseBody);

        }

        public void MakeRectangle(int bedValue,string responseBody)
        {
            var myBeds = FindBeds(responseBody);
          
            const int width = 75;
            const int height = 75;
            var top = 20;
            var left = 20;
            var tbTop = 100;

            //for (var i = 0; i < bedValue/2; i++)       
            //{
            //    var rec = new Rectangle
            //    {
            //        Width = width,
            //        Height = height,
            //        Fill = Brushes.AliceBlue,
            //        Stroke = Brushes.LightPink,
            //        StrokeThickness = 2

            //    };

            //    var tb = new TextBlock
            //    {
            //        Width = 75,
            //        Height = 80,
            //        Text = myBeds[i].Status.ToString() +" "+ myBeds[i].BedId

            //    };

            //    // Add to a canvas for example
            //    canvas.Children.Add(rec);
            //    canvas.Children.Add(tb);
            //    Canvas.SetTop(tb, tbTop);
            //    Canvas.SetLeft(tb, left);
            //    Canvas.SetTop(rec, top);
            //    Canvas.SetLeft(rec, left);
            //    left += 100;
            //}
            //top += 150;
            //tbTop += 150;
            //left = 20;

            //for (var i = bedValue/2; i < bedValue; i++)
            //{
            //    // Create the rectangle
            //    var rec = new Rectangle
            //    {
            //        Width = width,
            //        Height = height,
            //        Fill = Brushes.AliceBlue,
            //        Stroke = Brushes.LightPink,
            //        StrokeThickness = 2
            //    };

            //    var tb = new TextBlock
            //    {
            //        Width = 75,
            //        Height = 80,
            //        Text = myBeds[i].Status.ToString() + " " + myBeds[i].BedId

            //    };

            //    // Add to a canvas for example
            //    canvas.Children.Add(rec);
            //    canvas.Children.Add(tb);
            //    Canvas.SetTop(tb, tbTop);
            //    Canvas.SetLeft(tb, left);
            //    Canvas.SetTop(rec, top);
            //    Canvas.SetLeft(rec, left);
            //    left += 100;
            //}

            for (var i = 0; i < bedValue ; i++)
            {
                if (i == bedValue / 2)
                {
                    top += 150;
                    tbTop += 150;
                    left = 20;
                }
                var rec = new Rectangle
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.AliceBlue,
                    Stroke = Brushes.LightPink,
                    StrokeThickness = 2

                };

                var tb = new TextBlock
                {
                    Width = 75,
                    Height = 80,
                    Text = myBeds[i].Status.ToString() + " " + myBeds[i].BedId

                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, tbTop);
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(rec, top);
                Canvas.SetLeft(rec, left);
                left += 100;
            }
            

            


        }

        private void GetBeds_Click(object sender, RoutedEventArgs e)
        {
            _ = GetBeds_ClickAsync(sender, e);

        }

        public async System.Threading.Tasks.Task GetAlert_ClickAsync(object sender, RoutedEventArgs e)
        {
            var response = await new HttpClient().GetAsync("http://localhost:5000/api/PatientMonitoring");
            MessageBox.Show(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        private void GetAlert_Click(object sender, RoutedEventArgs e)
        {
             _ = GetAlert_ClickAsync(sender, e);
        }
        private void UndoAlert_Click(object sender, RoutedEventArgs e)
        {
            _ = GetAlert_ClickAsync(sender, e);
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
