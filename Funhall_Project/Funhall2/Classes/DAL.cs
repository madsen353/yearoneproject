using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Funhall2.Classes
{
    public class DAL
    {
        DBConnection dBConnection = new DBConnection();
        SqlCommand command = new SqlCommand();

        public  ObservableCollection<Customer> GetCustomers(Booking booking)
        {
            command.Connection = dBConnection.con;
            command.CommandType = CommandType.Text;
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            try
            {
                command.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;
                command.CommandText = "select * from Guests where BookingId = @bookingId";
                SqlDataReader reader;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer c = new Customer();
                    c.CusId = (int)reader[0];
                    c.BookingId = reader[1].ToString();
                    c.Name = reader[2].ToString(); ;
                    c.Email = reader[3].ToString();
                    c.Segway = reader.GetBoolean(4);
                    c.Subscription = reader.GetBoolean(5);
                    customers.Add(c);
                }
                    return customers;
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
        //Made by Rasmus
        //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");

        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            //Made by Rasmus
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        public void UpdateCusActivity(int id, string points, string activityName)
        {
            //Made by Rasmus
            //con.Open();
            // SqlCommand command = new SqlCommand("UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName", con);
            command.Connection = dBConnection.con;
            command.CommandText = "UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName";
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Points", points, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            command.ExecuteNonQuery();
            dBConnection.ConnectionClose();
            //con.Close();
        }
        public void EndActivity(string id, int isFinished, string activityName)
        {
            //Made by Rasmus
            //con.Open();
            // SqlCommand command = new SqlCommand("UPDATE BookedActivities SET IsFinished = @IsFinished WHERE BookingId = @ID AND TimeDesc = @ActivityName", con);
            command.CommandText = "UPDATE BookedActivities SET IsFinished = @IsFinished WHERE BookingId = @ID AND TimeDesc = @ActivityName";
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@IsFinished", isFinished, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            command.ExecuteNonQuery();
            dBConnection.ConnectionClose();
        }

        public string GetBookingIDFromGuestID(string id)
        {
            //Made by Rasmus
            string bookingId = "";
            //SqlCommand command = new SqlCommand("select BookingId FROM Guests WHERE GuestId='@ID', con");
            command.CommandType = CommandType.Text;
            command.Connection = dBConnection.con;
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.CommandText = "select BookingId FROM Guests WHERE GuestId='@ID'";
            SqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                bookingId = reader[0].ToString();
            }
            dBConnection.ConnectionClose();
            //con.Close();
            return bookingId;
        }

        public CustomerActivity getCusActivitySpecifiedByActivity(Customer cus, Activity act)
        {
            //Made by Rasmus
            //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
            //                                      + "Integrated Security=true;");
            //SqlCommand cmd = new SqlCommand();
            command.Connection = dBConnection.con;
            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;
            command.Parameters.Add("@Act", SqlDbType.NVarChar).Value = act.TimeDesc;

            command.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id AND ga.TimeDesc=@Act";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            SqlDataReader reader;
            reader = command.ExecuteReader();

            reader.Read();

            CustomerActivity ca = new CustomerActivity(cus,act);


                ca.Customer.CusId = (int)reader[0];
                ca.Activity.TimeDesc = reader[1].ToString();
                ca.Points = reader[2].ToString();
            //a.StartTime = DateTime.Parse(reader[2].ToString());
            //a.EndTime = DateTime.Parse(reader[3].ToString());
            dBConnection.ConnectionClose();
            return ca;
        }

        public List<int> GetTotalAmountOfPoints(int guestID)
        {
            //Made by Rasmus
            //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
            //                                      + "Integrated Security=true;");
            //SqlCommand cmd = new SqlCommand();
            command.Connection = dBConnection.con;
            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = guestID;

            command.CommandText = "select Points from GuestActivities where GuestId=@Id";
            List<int> allPoints = new List<int>();
            SqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string pointInString = reader[0].ToString();
                int point = int.Parse(pointInString);

                    allPoints.Add(point);
                
                }
            dBConnection.ConnectionClose();
            return allPoints;
        }

    }
}
