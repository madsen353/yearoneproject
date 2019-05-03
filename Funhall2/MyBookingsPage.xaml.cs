﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Funhall2
{
    /// <summary>
    /// Interaction logic for MyBookingsPage.xaml
    /// </summary>
    public partial class MyBookingsPage : Page
    {
        public MyBookingsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SegwayAnsvarsFragivelse window = new SegwayAnsvarsFragivelse();
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllBookingsPage allBookings = new AllBookingsPage();
            this.NavigationService.Navigate(allBookings);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AllBookingsPage allBookings = new AllBookingsPage();
            this.NavigationService.Navigate(allBookings);
        }
    }
}
