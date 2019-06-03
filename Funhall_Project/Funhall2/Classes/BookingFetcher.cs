using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Funhall2.Classes
{
    public class BookingFetcher
    {
        public List<Booking> GetBookingsFromRemoteServer()
        {
            //Made by Rasmus
            RemoteServerConfig config = ConfigurationReader.Read();
            RemoteServer server = new RemoteServer(config);
            List<Booking> _bookings = new List<Booking>();

            using (server)
            {
                _bookings = server.ReadAllBookings();
            }
            return _bookings;
        }
       
    }
}
