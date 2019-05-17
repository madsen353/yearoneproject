using Funhall2.Classes;
using System;
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
    public partial class GuestCheckinPage : Page
    {
        public Booking Booking { get; set; }

        public GuestCheckinPage(Booking booking)
        {
            //Made by Eby
            Booking = booking;
            InitializeComponent();
            n.Text = Booking.name;

        }

        private void AddGuestToDb(object sender, RoutedEventArgs e)
        {
            //Made by Eby
            Customer cus = new Customer();
            cus.BookingId = Booking.flexyId;
            cus.Name = Name.Text;
            cus.Email = Email.Text;
            if (Subscription.IsChecked == true)
                cus.Subscription = true;
            else
                cus.Subscription = false;
            
            if (Segway.IsChecked == true)
                  cus.Segway = true;
            else
                cus.Segway = false;

            DAL.CheckInCus(cus);
            DAL.AddActivities(cus);
            AllBookingsPage AllBookings = new AllBookingsPage();
            this.NavigationService.Navigate(AllBookings);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Made by Anders & Niels
            SegwayAnsvarsFragivelse window = new SegwayAnsvarsFragivelse();
            window.ShowDialog();
        }
    }
}
