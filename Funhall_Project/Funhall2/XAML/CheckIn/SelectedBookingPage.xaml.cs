using Funhall2.Classes;
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
using Funhall2.XAML.PointSystem;

namespace Funhall2
{
    
    public partial class SelectedBookingPage : Page
    {
        DAL dal = new DAL();
        public  Booking Booking { get; set; }
        public string globalPageHistory = "";
        public SelectedBookingPage(Booking booking, string pageHistory)
        {
            globalPageHistory = pageHistory;
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
                Profile page = new Profile(cus, b, globalPageHistory);
                this.NavigationService.Navigate(page);
            }
            else
            {
                MessageBox.Show("Vælg en Guest fra listen");

            }
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            if (globalPageHistory == "AllBookingsPage")
            {
                AllBookingsPage AllBookings = new AllBookingsPage();
                this.NavigationService.Navigate(AllBookings);
            }
            else if (globalPageHistory == "AllActivities")
            {
                AllActivities allActivities = new AllActivities(Booking);
                this.NavigationService.Navigate(allActivities);
            }
            }
    }
}
