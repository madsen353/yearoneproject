using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace CheckInApp
{
    //Made by Eby
    public class DAL
    {
        public void InsertBookingToDb(RawBooking booking)
        {

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Clear();
            if (booking.flexyId != null && booking.name != null && booking.cusTel != null)
            {
                string BookingId = booking.flexyId;
                string Name = booking.name;
                string CusTel = booking.cusTel;

                //string fax = Attractions[i].ContactInformation.Fax;
                //string email = Attractions[i].ContactInformation.Email;
                AddParam(cmd, booking.flexyId, "BookingId", SqlDbType.NVarChar);
                AddParam(cmd, booking.name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, booking.cusTel, "CusTel", SqlDbType.NVarChar);
                //AddParam(cmd, fax, "Fax", SqlDbType.NVarChar);
                //AddParam(cmd, email, "Email", SqlDbType.NVarChar);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Bookings (BookingId, Name, CusTel)" +
                    " values (@BookingId, @Name, @CusTel)";
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            con.Close();
        }

        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + name;
            if (value != null)
            {
                parameter.Value = value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            parameter.SqlDbType = sqlDbType;
            parameter.Size = 255;
            cmd.Parameters.Add(parameter);
        }


    
}
}
