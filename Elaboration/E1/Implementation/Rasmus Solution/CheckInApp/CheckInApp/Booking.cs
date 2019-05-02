using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInApp
{
    public class Booking
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime StartTimeOfBooking { get; set; }
        public int AmountOfGuests { get; set; }
        public List<string> Activities { get; set; }
    }
}
