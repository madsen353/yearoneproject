using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Funhall2
{
    public class DALBooking
    {
        //Made by Eby
        
        public static ObservableCollection<Booking> getBookings()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Bookings";
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Booking b = new Booking();
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
                bookings.Add(b);
            }
            con.Close();
            return bookings;
        }
    }
}
