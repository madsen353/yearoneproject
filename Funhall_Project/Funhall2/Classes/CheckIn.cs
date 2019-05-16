using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Funhall2.Classes
{
    public class CheckIn //Made by Eby
    {
        public Customer Cus { get; set; }

        DBConnection dBConnection = new DBConnection();
        SqlCommand cmd = new SqlCommand();

        public  void CheckInCus(Customer cus) //Made by Eby
        {
            cmd.Connection = dBConnection.con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "insert into Guests (BookingId, Name, Email, AgreeTerms, Subscription) values " +
                                     "(@BookingId, @Name, @Email, @AgreeTerms, @Subscription)";

            cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;

            cmd.ExecuteNonQuery();
            dBConnection.ConnectionClose();
        }

        public  void UpdateCus(Customer cus) //Made by Eby
        {           
            cmd.Connection = dBConnection.con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Update Guests set  Name=@Name, Email=@Email, AgreeTerms=@AgreeTerms," +
                "Subscription=@Subscription";

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;

            cmd.ExecuteNonQuery();
            dBConnection.ConnectionClose();
        }

        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            //Made by Eby
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            parameter.SqlDbType = sqlDbType;
            parameter.Size = 255;
            cmd.Parameters.Add(parameter);
        }
    }
}
