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

namespace AlertToCareAPIUI
{
    /// <summary>
    /// Interaction logic for InitialScreen.xaml
    /// </summary>
    public partial class InitialScreen : Window
    {
        public InitialScreen()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new ICUDetailsWindow();
            newWind.Show();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new ICURemove();
            newWind.Show();
        }

        private void Parallel_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new ParallelLayout();
            newWind.Show();
        }
    }
}
