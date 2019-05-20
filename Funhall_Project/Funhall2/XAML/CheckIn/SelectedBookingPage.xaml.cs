﻿using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class SelectedBookingPage : Page
    {
        DAL dal = new DAL();
        public  Booking Booking { get; set; }
        public SelectedBookingPage(Booking booking)
        {
            //Made by Eby
            Booking = booking;
            InitializeComponent();            
            this.DataContext = Booking;

            ObservableCollection<Activity> activities = dal.GetBookedActivities(Booking);
            Activities.ItemsSource = activities;

            ObservableCollection<Customer> guests = dal.GetCustomers(Booking);
            Guests.ItemsSource = guests;
        }

        private void ViewGuestInfo(object sender, MouseButtonEventArgs e)
        {
            //Made by Eby
            Booking b =  Booking;
            if (b != null)
            {
                Customer cus = Guests.SelectedItem as Customer;
                Profile page = new Profile(cus, b);
                this.NavigationService.Navigate(page);
            }
            else
            {
                MessageBox.Show("Vælg en Guest fra listen");

            }
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            AllBookingsPage page = new AllBookingsPage();
            this.NavigationService.Navigate(page);
        }
    }
}
