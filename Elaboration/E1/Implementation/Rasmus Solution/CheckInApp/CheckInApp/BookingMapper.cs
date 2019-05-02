using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInApp
{
    public class BookingMapper
    {
        public List<Booking> Map(List<RawBooking> rawBookings)
        {
            List<Booking> bookings = new List<Booking>();

            foreach (var rawBooking in rawBookings)
            {
                Booking booking = new Booking
                {
                    ID = rawBooking.flexyId,
                    Name = rawBooking.name,
                    StartTimeOfBooking = StringToDateTime(rawBooking.times.First().start),
                    AmountOfGuests = StringToInt(rawBooking.products.First().prodAmount),
                    Activities = GetActivities(rawBooking.products)

                };
                bookings.Add(booking);
            }
            return bookings;
        }
        private List<string> GetActivities(List<RawBooking.Product> activitiesFromRawBooking)
        {
            List<string> activities = new List<string>();
            foreach (var activity in activitiesFromRawBooking)
            {
                activities.Add(activity.product);
            }
            return activities;
        }
        private DateTime StringToDateTime(string str)
        {
            return DateTime.Parse(str);
        }

        private int StringToInt(string str)
        {
            if (str.Contains(",") == true)
            {
                int index = str.IndexOf(",");
                str = str.Substring(0, index);
            }
            return int.Parse(str);
        }

    }
}
