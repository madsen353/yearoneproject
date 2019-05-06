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
    /// <summary>
    /// Interaction logic for AllBookingsPage.xaml
    /// </summary>
    public partial class AllBookingsPage : Page
    {
        public AllBookingsPage()
        {
            InitializeComponent();
            DataContext = this;
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
            //this.Close(); virker ikke på en page.
            //List<Activity> bookedActivities = Activity.getBookedActivities("fb2123");

            //Made by Rasmus

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
    }
}
