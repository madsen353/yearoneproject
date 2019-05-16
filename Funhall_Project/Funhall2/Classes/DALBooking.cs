using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Funhall2
{
    public class DALBooking //Made by Eby
    {          
       
        DBConnection dBConnection = new DBConnection();
        SqlCommand cmd = new SqlCommand(); 
        
        public ObservableCollection<Booking> getBookings() //Made by Eby
        {
            cmd.Connection = dBConnection.con;            

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Bookings";
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Booking b = new Booking();
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
                bookings.Add(b);
            }
            dBConnection.ConnectionClose();
            return bookings;
        }

        public  Booking getSelectedBookingData(Booking booking)
        {
            //Made by Eby
            cmd.Connection = dBConnection.con;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;

            cmd.CommandText = "select * from Bookings where BookingId =@bookingId";
            Booking b = new Booking();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
            }
            dBConnection.ConnectionClose();
            return b;
        }

        public Booking getBookingData(string id) //Made by Eby
        {
            cmd.Connection = dBConnection.con;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = id;

            cmd.CommandText = "select * from Bookings where BookingId =@bookingId";
            Booking b = new Booking();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                b.flexyId = reader[0].ToString(); ;
                b.name = reader[1].ToString();
                b.date = reader[5].ToString();
            }
            dBConnection.ConnectionClose();
            return b;
        }
    }
}
