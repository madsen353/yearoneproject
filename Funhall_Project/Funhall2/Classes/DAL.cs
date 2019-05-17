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
        //REFACTOR THIS!!
        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            //Made by Eby
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

        //Made by Rasmus
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
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
            //Made by Rasmus
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
            //Made by Rasmus
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
            //Made by Rasmus
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

        public List<int> GetTotalAmountOfPoints(int guestID)
        {
            //Made by Rasmus
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = guestID;

            cmd.CommandText = "select Points from GuestActivities where GuestId=@Id";
            List<int> allPoints = new List<int>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string pointInString = reader[0].ToString();
                int point = int.Parse(pointInString);

                    allPoints.Add(point);
                
                }
            con.Close();
            return allPoints;
        }

        public static ObservableCollection<Activity> getBookedActivities(Booking booking)
        {
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;

            cmd.CommandText = "select * from BookedActivities where BookingId = @bookingId";
            ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Activity a = new Activity();
                a.BookingId = reader[0].ToString();
                a.TimeDesc = reader[1].ToString();
                a.StartTime = DateTime.Parse(reader[2].ToString());
                a.EndTime = DateTime.Parse(reader[3].ToString());
                a.IsFinished = (int)reader[4];
                activities.Add(a);
            }
            con.Close();
            return activities;
        }

        public static void InsertBookingToDb(Booking booking)
        {//Made by Eby
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
                string CusTelAlt = booking.cusTelAlt;
                string CusMail = booking.cusMail;
                string Date = booking.date;
                string TotalPrice = booking.totalPrice;
                string CusComment = booking.cusComment;
                string IntComment = booking.intComment;

                AddParam(cmd, BookingId, "BookingId", SqlDbType.NVarChar);
                AddParam(cmd, Name, "Name", SqlDbType.NVarChar);
                AddParam(cmd, CusTel, "CusTel", SqlDbType.NVarChar);
                AddParam(cmd, CusTelAlt, "CusTelAlt", SqlDbType.NVarChar);
                AddParam(cmd, CusMail, "CusMail", SqlDbType.NVarChar);
                AddParam(cmd, Date, "Date", SqlDbType.NVarChar);
                AddParam(cmd, TotalPrice, "TotalPrice", SqlDbType.NVarChar);
                AddParam(cmd, CusComment, "CusComment", SqlDbType.NVarChar);
                AddParam(cmd, IntComment, "intComment", SqlDbType.NVarChar);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Bookings (BookingId, Name, CusTel, CusTelAlt, Cusmail, Date, TotalPrice, CusComment, IntComment)" +
                    " values (@BookingId, @Name, @CusTel, @CusTelAlt, @Cusmail, @Date, @TotalPrice, @CusComment, @IntComment)";
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added Successfully!");
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);

                }
            }
            con.Close();
        }

        public static void InsertBookedActivitiesToDb(Booking booking)
        {
            List<Booking.Time> times = booking.times;
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            foreach (var time in times)
            {
                if (!time.timeDesc.Equals("#"))
                {

                    cmd.Parameters.Clear();

                    string FlexyId = booking.flexyId;
                    string Desc = time.timeDesc;
                    string StartTime = time.start;
                    string Endtime = time.end;
                    int IsFinished = 0;

                    AddParam(cmd, FlexyId, "BookingId", SqlDbType.NVarChar);
                    AddParam(cmd, Desc, "Desc", SqlDbType.NVarChar);
                    AddParam(cmd, StartTime, "StartTime", SqlDbType.NVarChar);
                    AddParam(cmd, Endtime, "Endtime", SqlDbType.NVarChar);
                    AddParam(cmd, IsFinished, "IsFinished", SqlDbType.Int);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into BookedActivities(BookingId, TimeDesc, StartTime, Endtime, IsFinished)" +
                                      "values (@BookingId, @Desc, @StartTime, @Endtime, @IsFinished)";

                    try
                    {
                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }
        public static void InsertActivityToDb(Booking booking)
        {//Made by Eby
            List<Booking.Time> times = booking.times;
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            foreach (var time in times)
            {
                if (!time.timeDesc.Equals("#"))
                {

                    cmd.Parameters.Clear();
                    string Desc = time.timeDesc;
                    AddParam(cmd, Desc, "Desc", SqlDbType.NVarChar);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Activities (Description)" +
                                      " values (@Desc)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
            con.Close();
        }
        public static void InsertBookedProductsToDb(Booking booking)
        {
            List<Booking.Product> products = booking.products;
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                                  + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            foreach (var product in products)
            {
                if (!product.product.Equals("#"))
                {

                    cmd.Parameters.Clear();
                    string FlexyId = booking.flexyId;
                    string ProductDesc = product.product;
                    string productPrice = product.prodPrice;
                    string productTotPrice = product.prodTotPrice;
                    string productAmount = product.prodAmount;

                    AddParam(cmd, FlexyId, "BookingId", SqlDbType.NVarChar);
                    AddParam(cmd, ProductDesc, "ProductDesc", SqlDbType.NVarChar);
                    AddParam(cmd, productPrice, "productPrice", SqlDbType.NVarChar);
                    AddParam(cmd, productTotPrice, "productTotPrice", SqlDbType.NVarChar);
                    AddParam(cmd, productAmount, "productAmount", SqlDbType.NVarChar);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into BookedProducts (BookingId, ProductDesc, productPrice, productTotPrice, productAmount)" +
                                      " values (@BookingId, @ProductDesc, @productPrice, @productTotPrice, @productAmount)";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added Successfully!");
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
            }

            con.Close();
        }

    }
}
