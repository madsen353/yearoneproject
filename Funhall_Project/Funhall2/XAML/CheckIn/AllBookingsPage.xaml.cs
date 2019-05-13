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

            foreach (var booking in _bookings)
            {
                booking.InsertBookingToDb();
                booking.InsertBookedActivitiesToDb();
                booking.InsertActivityToDb();
                booking.InsertBookedProductsToDb();
            }


            //Made by Rasmus

            InitializeComponent();
            DataContext = this;

            //ListBox Solution:
            ObservableCollection<Booking> bookings = DALBooking.getBookings();
            listBox.ItemsSource = bookings;
            //DataGrid solution:
            //ObservableCollection<Booking> bookings = DALBooking.getBookings();
            //grid.ItemsSource = bookings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckInPage checkIn = new CheckInPage();
            this.NavigationService.Navigate(checkIn);
        }

       

        private void ShowSelectedBooking(object sender, RoutedEventArgs e)
        {
            Booking b = listBox.SelectedItem as Booking;
            SelectedBookingPage page = new SelectedBookingPage(b);
            this.NavigationService.Navigate(page);
        }

        private void Checkin_Click(object sender, RoutedEventArgs e)
        {
            Booking b = listBox.SelectedItem as Booking;
            GuestCheckinPage page = new GuestCheckinPage(b);
            this.NavigationService.Navigate(page);
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            CheckInPage CheckIn = new CheckInPage();
            this.NavigationService.Navigate(CheckIn);
        }
    }
}
