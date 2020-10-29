using System;
using System.Diagnostics;
using System.Windows;


namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for InitialScreen.xaml
    /// </summary>
    public partial class InitialScreen
    {
        public InitialScreen()
        {
            InitializeComponent();
            var newProcess = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    FileName = System.IO.Directory
                        .GetParent(System.IO.Directory.GetParent(
                            System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString())
                         + @"\AlertToCareAPI\bin\Debug\netcoreapp3.1\AlertToCareAPI.exe"
                }

        
            };
            newProcess.Start();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new ICUDetailsWindow();
            newWind.Show();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new IcuRemove();
            newWind.Show();
        }

        private void Parallel_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new ParallelLayout();
            newWind.Show();
        }

        private void LShaped_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new LShapedLayout();
            newWind.Show();
        }
        
    }
}
