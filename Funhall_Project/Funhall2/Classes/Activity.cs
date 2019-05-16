using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        DBConnection dBConnection = new DBConnection();
        SqlCommand cmd = new SqlCommand();

        public  ObservableCollection<Activity> getBookedActivities(Booking booking)  //Made by Eby

        {
            cmd.Connection = dBConnection.con;
            cmd.CommandType = CommandType.Text;
            ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
            try
            {
                cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;

                cmd.CommandText = "select * from BookedActivities where BookingId = @bookingId";
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Activity a = new Activity();
                    a.BookingId = reader[0].ToString();
                    a.TimeDesc = reader[1].ToString();
                    a.StartTime = DateTime.Parse(reader[2].ToString());
                    a.EndTime = DateTime.Parse(reader[3].ToString());
                    //a.IsFinished = (int)reader[4];
                    activities.Add(a);
                }
             return activities;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                 dBConnection.ConnectionClose();
            }
        }
    }
}
