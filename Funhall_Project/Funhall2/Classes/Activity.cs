using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funhall2.Classes;

namespace Funhall2
{
    //Made by Eby
    public class Activity
    {
        public string BookingId { get; set; }
        public string TimeDesc { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int IsFinished { get; set; }

        public static ObservableCollection<Activity> getBookedActivities(Booking booking)
        {
            //Made by Rasmus
            ObservableCollection<Activity> activities = DAL.getBookedActivities(booking);
            return activities;
        }
    }
}
