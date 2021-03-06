﻿using System;
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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.SelectedFuel = (FuelType)((Button)sender).DataContext;
            NavigationService.Navigate(new Uri("/Fillup.xaml", UriKind.Relative));
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            App.ViewModel.RecalcAll();
        }
    }
}