using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace E85_Helper
{
    public partial class Fillup : PhoneApplicationPage
    {
        public Fillup()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel.SelectedFuel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.Car.FillUpTank(App.ViewModel.SelectedFuel, System.Convert.ToSingle(GallonsBox.Text), System.Convert.ToInt32(DistanceBox.Text));
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                
            }
        }
    }
}