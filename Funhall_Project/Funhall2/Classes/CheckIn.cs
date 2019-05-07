using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2.Classes
{
    public class CheckIn
    {
        public Customer Cus { get; set; }

        public static void CheckInCus(Customer cus)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into Guests (BookingId, Name, Email, AgreeTerms, Subscription) values " +
                                     "(@BookingId, @Name, @Email, @AgreeTerms, @Subscription)";

            cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void UpdateCus(Customer cus)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Update Guests set  Name=@Name, Email=@Email, AgreeTerms=@AgreeTerms," +
                "Subscription=@Subscription";

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            parameter.SqlDbType = sqlDbType;
            parameter.Size = 255;
            cmd.Parameters.Add(parameter);
        }
    }
}
