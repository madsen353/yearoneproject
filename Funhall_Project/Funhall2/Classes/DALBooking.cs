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
            //Made by Eby
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

        public static Booking getSelectedBookingData(Booking booking)
        {
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;

            cmd.CommandText = "select * from Bookings where BookingId =@bookingId";
            Booking b = new Booking();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
            }
            con.Close();
            return b;
        }

        public static Booking getBookingData(string id)
        {
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = id;

            cmd.CommandText = "select * from Bookings where BookingId =@bookingId";
            Booking b = new Booking();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
            }
            con.Close();
            return b;
        }
    }
}
