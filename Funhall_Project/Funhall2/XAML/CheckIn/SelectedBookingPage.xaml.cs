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
        public  Booking Booking { get; set; }
        public SelectedBookingPage(Booking booking)
        {
            Booking = booking;
            InitializeComponent();            
            this.DataContext = Booking;

            ObservableCollection<Activity> activities = Activity.getBookedActivities(Booking);
            Activities.ItemsSource = activities;

            ObservableCollection<Customer> guests = Customer.GetCustomers(Booking);
            Guests.ItemsSource = guests;
        }

        private void ViewGuestInfo(object sender, MouseButtonEventArgs e)
        {
            Booking b =  Booking;
            Customer cus = Guests.SelectedItem as Customer;
            Profile page = new Profile(cus,b);
            this.NavigationService.Navigate(page);

        }
    }
}