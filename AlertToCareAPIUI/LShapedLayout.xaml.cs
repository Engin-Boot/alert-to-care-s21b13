using AlertToCareAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for LShapedLayout.xaml
    /// </summary>
    public partial class LShapedLayout
    {
        public LShapedLayout()
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
            MakeRectangle(JsonConvert.DeserializeObject<ICUBedDetails>(responseBody).BedsCount,responseBody);

        }

        public void MakeRectangle(int bedValue, string responseBody)
        {
            var myBeds = FindBeds(responseBody);

            const int width = 35;
            const int height = 35;
            var top = 20;
            var left = 20;
            var tbTop = 65;

            for (var i = 0; i < bedValue / 2; i++)
            {
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
                    Text = myBeds[i].Status + " " + myBeds[i].BedId

                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, tbTop);
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(rec, top);
                Canvas.SetLeft(rec, left);
                left += 70;
            }
            top += 100;
            tbTop += 90;
            left = 20;

            for (var i = bedValue / 2; i < bedValue; i++)
            {
                // Create the rectangle
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
                    Text = myBeds[i].Status + " " + myBeds[i].BedId

                };

                // Add to a canvas for example
                canvas.Children.Add(rec);
                canvas.Children.Add(tb);
                Canvas.SetTop(tb, tbTop);
                Canvas.SetLeft(tb, left);
                Canvas.SetTop(rec, top);
                Canvas.SetLeft(rec, left);
                top += 60;
                tbTop += 60;
            }

            //for (var i = 0; i < bedValue; i++)
            //{
            //    if (i == bedValue / 2)
            //    {
            //        top += 100;
            //        tbTop += 90;
            //        left = 20;
            //    }


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
            //    //left += 100;
            //    if (i < bedValue / 2) {left+=70;continue;}
            //    top += 60;
            //    tbTop += 60;
            //}


        }

        private void GetBeds_Click(object sender, RoutedEventArgs e)
        {
            _ = GetBeds_ClickAsync(sender, e);

        }

        public async System.Threading.Tasks.Task GetAlert_ClickAsync(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/api/PatientMonitoring");
            
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            MessageBox.Show(responseBody);
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
