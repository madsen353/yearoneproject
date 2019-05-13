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
    public class DAL
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        public void UpdateCusActivity(int id, string points, string activityName)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName", con);
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Points", points, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            command.ExecuteNonQuery();
            con.Close();
        }
        public void EndActivity(string id, int isFinished, string activityName)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE BookedActivities SET IsFinished = @IsFinished WHERE BookingId = @ID AND TimeDesc = @ActivityName", con);
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@IsFinished", isFinished, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            command.ExecuteNonQuery();
            con.Close();
        }

        public string GetBookingIDFromGuestID(string id)
        {
        string bookingId = "";
        SqlCommand command = new SqlCommand("select BookingId FROM Guests WHERE GuestId='@ID', con");
        command.CommandType = CommandType.Text;
        SqlDataReader reader;
        command.Connection = con;
        command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
        con.Open();
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            bookingId = reader[0].ToString();
        }
        con.Close();
        return bookingId;
        }

        public CustomerActivity getCusActivitySpecifiedByActivity(Customer cus, Activity act)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;
            cmd.Parameters.Add("@Act", SqlDbType.NVarChar).Value = act.TimeDesc;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id AND ga.TimeDesc=@Act";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            con.Open();
            reader = cmd.ExecuteReader();

            reader.Read();

            CustomerActivity ca = new CustomerActivity(cus,act);


                ca.Customer.CusId = (int)reader[0];
                ca.Activity.TimeDesc = reader[1].ToString();
                ca.Points = reader[2].ToString();
                //a.StartTime = DateTime.Parse(reader[2].ToString());
                //a.EndTime = DateTime.Parse(reader[3].ToString());
                con.Close();
                return ca;
                
            

        }

    }
}
