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
    /// <summary>
    /// Interaction logic for CheckInPage.xaml
    /// </summary>
    public partial class CheckInPage : Page
    {
        public CheckInPage()
        {
            InitializeComponent();
        }
       


        private void Button_Click(object sender, RoutedEventArgs e)
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
            //this.Close(); virker ikke på en page.
            List<Booking> bookings = DALBooking.getBookings();
            //List<Activity> bookedActivities = Activity.getBookedActivities("fb2123");

            //Made by Anders & Niels
            AllBookingsPage AllBookings = new AllBookingsPage();
            this.NavigationService.Navigate(AllBookings);
        }
    }
}
