//using GetDataFromLinuxServer.ServerConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckInAppVS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckIn(object sender, RoutedEventArgs e)
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
            this.Close();
            List<Booking> bookings = DALBooking.getBookings();
            List<Activity> bookedActivities = Activity.getBookedActivities("fb2123");
        }

    }
    
}
