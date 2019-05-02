
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace FunHall
{

    public class Booking
    {
        public string flexyId { get; set; }
        public string name { get; set; }
        public string cusTel { get; set; }
        public string quoteBooking { get; set; }
        public string cusTelAlt { get; set; }
        public string cusMail { get; set; }
        public string date { get; set; }
        public Time[] times { get; set; }
        public Product[] products { get; set; }
        public string totalPrice { get; set; }
        public string cusComment { get; set; }
        public string intComment { get; set; }

       

        public void InsertBookingToDb()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Clear();
            if (flexyId != null && name !=null && cusTel !=null)
            {
                string BookingId = flexyId;
                string Name = name;
                string CusTel = cusTel;
                
                //string fax = Attractions[i].ContactInformation.Fax;
                //string email = Attractions[i].ContactInformation.Email;
                AddParam(cmd, flexyId, "BookingId", SqlDbType.NVarChar);
                AddParam(cmd, name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, cusTel, "CusTel", SqlDbType.NVarChar);
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


