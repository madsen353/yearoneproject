using System;
using System.Collections.Generic;
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

        public static List<Activity> getBookedActivities(string id)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = id;

            cmd.CommandText = "select * from BookedActivities where BookingId = @bookingId";
            List<Activity> activities = new List<Activity>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Activity a = new Activity();
                a.BookingId = reader[0].ToString();
                a.TimeDesc = reader[1].ToString();
                a.StartTime = DateTime.Parse(reader[2].ToString());
                a.EndTime = DateTime.Parse(reader[3].ToString());
                activities.Add(a);
            }
            con.Close();
            return activities;
        }
    }
}
