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
using Funhall2.Classes;

namespace Funhall2
{
    
    public partial class AllBookingsPage : Page
    {
        public AllBookingsPage()
        {
            
            //Made by Rasmus

            RemoteServerConfig config = ConfigurationReader.Read();
            RemoteServer server = new RemoteServer(config);
            List<Booking> _bookings = new List<Booking>();
            using (server)
            {
                _bookings = server.ReadAllBookings();
            }

            //Made by Eby
            //Refactored by Rasmus(methods moved from Booking class to DAL class.
            
            foreach (var booking in _bookings)
            {
                DAL.InsertBookingToDb(booking);
                DAL.InsertBookedActivitiesToDb(booking);
                DAL.InsertActivityToDb(booking);
                DAL.InsertBookedProductsToDb(booking);
                
            }


            //Made by Rasmus

            InitializeComponent();
            DataContext = this;

            //ListBox Solution:
            ObservableCollection<Booking> bookings = DALBooking.getBookings();
            listBox.ItemsSource = bookings;
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Made by Anders & Niels
            CheckInPage checkIn = new CheckInPage();
            this.NavigationService.Navigate(checkIn);
        }

       

        private void ShowSelectedBooking(object sender, RoutedEventArgs e)
        {
            //Made by Eby
            Booking b = listBox.SelectedItem as Booking;
            SelectedBookingPage page = new SelectedBookingPage(b);
            this.NavigationService.Navigate(page);
        }

        private void Checkin_Click(object sender, RoutedEventArgs e)
        {
            //Made by Eby
            Booking b = listBox.SelectedItem as Booking;
            GuestCheckinPage page = new GuestCheckinPage(b);
            this.NavigationService.Navigate(page);
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            CheckInPage CheckIn = new CheckInPage();
            this.NavigationService.Navigate(CheckIn);
        }
    }
}
