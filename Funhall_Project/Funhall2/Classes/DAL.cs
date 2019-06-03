using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Funhall2.Classes;

namespace Funhall2.Classes
{
    public class DAL : IDAL
    {
        //Made by Rasmus
        private SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
        private SqlCommand cmd = new SqlCommand();

        public DAL()
        {
            //Made by Rasmus
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
        }
        public void ResetDAL()
        {
            //Made by Rasmus
            con.Close();
            con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
            cmd = new SqlCommand {Connection = con, CommandType = CommandType.Text};
        }

        public void OpenConnection(SqlConnection con)
        {
                con.Open();
        }
        
        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            //Made by Rasmus
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }

        //Made by Rasmus
        public void UpdateCusActivity(int id, string points, string activityName)
        {
            //Made by Rasmus
            OpenConnection(con);
                cmd.CommandText = "UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName";
                cmd.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
                cmd.Parameters.Add(CreateParam("@Points", points, SqlDbType.Int));
                cmd.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
                cmd.ExecuteNonQuery();
                ResetDAL();
        }
        public void EndActivity(string id, bool isFinished, string activityName)
        {
            //Made by Rasmus
            OpenConnection(con);
                cmd.CommandText =
                    "UPDATE BookedActivities SET IsFinished = @IsFinished WHERE BookingId = @ID AND TimeDesc = @ActivityName";
                cmd.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
                cmd.Parameters.Add(CreateParam("@IsFinished", isFinished, SqlDbType.Bit));
                cmd.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
                cmd.ExecuteNonQuery();
                ResetDAL();
        }
        public CustomerActivity GetCusActivitySpecifiedByActivity(Customer cus, Activity act)
        {
            //Made by Rasmus
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;
            cmd.Parameters.Add("@Act", SqlDbType.NVarChar).Value = act.TimeDesc;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id AND ga.TimeDesc=@Act";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            OpenConnection(con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    CustomerActivity ca = new CustomerActivity(cus, act);
                    ca.Customer.CusId = (int) reader[0];
                    ca.Activity.TimeDesc = reader[1].ToString();
                    ca.Points = (int) reader[2];
                    //a.StartTime = DateTime.Parse(reader[2].ToString());
                    //a.EndTime = DateTime.Parse(reader[3].ToString());
                    ResetDAL();
                    return ca;
                }
        }

        public List<int> GetTotalAmountOfPoints(int guestID)
        {
            //Made by Rasmus
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = guestID;
            cmd.CommandText = "select Points from GuestActivities where GuestId=@Id";
            List<int> allPoints = new List<int>();
            OpenConnection(con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //string pointInString = reader[0].ToString();
                    //int point = int.Parse(pointInString);
                    int point = (int) reader[0];
                    allPoints.Add(point);
                }
            }
            ResetDAL();
            return allPoints;
                
        }

        public ObservableCollection<Activity> GetBookedActivities(Booking booking)
        {
            //Made by Eby
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;
            cmd.CommandText = "select * from BookedActivities where BookingId = @bookingId";
            ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
            OpenConnection(con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Activity a = new Activity();
                        a.BookingId = reader[0].ToString();
                        a.TimeDesc = reader[1].ToString();
                        string start = reader[2].ToString();
                        if (!string.IsNullOrEmpty(start))
                        {
                            a.StartTime = DateTime.Parse(start);
                        }
                        string end = reader[3].ToString();
                        if (!string.IsNullOrEmpty(end))
                        {
                            a.EndTime = DateTime.Parse(end);
                        }                        
                            a.IsFinished = (bool)reader.GetBoolean(4);                    

                        
                        activities.Add(a);
                    }
                }

            ResetDAL();
            return activities;
        }
        public void CheckInCus(ICustomer cus)
        {
            //Made by Eby
            
            cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;
            cmd.Parameters.Add("@CheckedInTime", SqlDbType.DateTime).Value = DateTime.Now;

            // check for duplicate email
            OpenConnection(con);
            cmd.CommandText = "select * from Guests where Email = @Email and BookingId = @BookingId ";
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (count > 0)
            {
                //MessageBox.Show("Email already exists");
                throw new System.ArgumentException("Du er allerede checked ind.");
            }
            else
            {                   
                cmd.CommandText = "insert into Guests (BookingId, Name, Email, AgreeTerms, Subscription) values " +
                                  "(@BookingId, @Name, @Email, @AgreeTerms, @Subscription)";
                
                try
                {
                    OpenConnection(con);
                    cmd.ExecuteNonQuery();
                    ResetDAL();
                }
                catch (Exception)
                {
                    //throw;
                }               
                
            }
        }

        public void UpdateCus(Customer cus)
        {
            //Made by Eby
            cmd.CommandText = "Update Guests set  Name=@Name, Email=@Email, AgreeTerms=@AgreeTerms," +
                              "Subscription=@Subscription where GuestId=@cusId";

            cmd.Parameters.Add("@cusId", SqlDbType.Int).Value = cus.CusId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;
            OpenConnection(con);
            cmd.ExecuteNonQuery();
            ResetDAL();
            
        }
        public void AddActivities(ICustomer cus)
        {
            //Made by Eby
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            //cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;       
            cmd.CommandText = "INSERT INTO GuestActivities (GuestId, TimeDesc, Points) " +
                              "SELECT  g.GuestId, b.TimeDesc, 0" +
                              "FROM BookedActivities b" +
                              " inner JOIN Guests g on g.BookingId = b.BookingId " +
                              "where g.Email=@Email";
            OpenConnection(con);
                cmd.ExecuteNonQuery();
                ResetDAL();
        }
        public ObservableCollection<CustomerActivity> GetCusActivities(Customer cus)
        {
            //Made by Eby
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            OpenConnection(con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerActivity ca = new CustomerActivity();
                        ca.Customer.CusId = (int) reader[0];
                        ca.Activity.TimeDesc = reader[1].ToString();
                        ca.Points = (int) reader[2];
                        //a.StartTime = DateTime.Parse(reader[2].ToString());
                        //a.EndTime = DateTime.Parse(reader[3].ToString());
                        cusActivities.Add(ca);
                    }
                }

                ResetDAL();
                return cusActivities;
            
        }
        public ObservableCollection<Customer> GetCustomers(Booking booking)
        {
            //Made by Eby
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;
            cmd.CommandText = "select * from Guests where BookingId = @bookingId";
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            OpenConnection(con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer c = new Customer();
                        c.CusId = (int) reader[0];
                        c.BookingId = reader[1].ToString();
                        c.Name = reader[2].ToString();
                        ;
                        c.Email = reader[3].ToString();
                        c.Segway = reader.GetBoolean(4);
                        c.Subscription = reader.GetBoolean(5);
                        c.TotalAmountOfPoints = c.GetTotalAmountOfPoints();
                        customers.Add(c);
                    }
                }
                ResetDAL();
                return customers;
        }

        public ObservableCollection<Booking> getBookings()
        {
            //Made by Eby
            //cmd.CommandText = "select * from Bookings ";
            cmd.CommandText = "SELECT* FROM Bookings b where b.BookingId in " +
                              "(SELECT DISTINCT ba.BookingId from BookedActivities ba  where ba.IsFinished = 'false' )";
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            OpenConnection(con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Booking b = new Booking();
                    b.flexyId = reader[0].ToString();
                    ;
                    b.name = reader[1].ToString();
                    b.date = reader[5].ToString();
                    bookings.Add(b);
                }
            }

            ResetDAL();
            return bookings;
        }
        //If there is time, the methods below should be rewritten. They are using a duplicate Param method.

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
        public void InsertBookingToDb(Booking booking)
        {//Made by Eby
            OpenConnection(con);
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

                    cmd.CommandText =
                        "insert into Bookings (BookingId, Name, CusTel, CusTelAlt, Cusmail, Date, TotalPrice, CusComment, IntComment)" +
                        " values (@BookingId, @Name, @CusTel, @CusTelAlt, @Cusmail, @Date, @TotalPrice, @CusComment, @IntComment)";
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

                ResetDAL();
        }
        public void InsertBookedActivitiesToDb(Booking booking)
        {
            List<Booking.Time> times = booking.times;
            //Made by Eby
            OpenConnection(con);
                foreach (var time in times)
                {
                    if (!time.timeDesc.Equals("#"))
                    {
                        cmd.Parameters.Clear();
                        string FlexyId = booking.flexyId;
                        string Desc = time.timeDesc;
                        string StartTime = time.start;
                        string Endtime = time.end;
                        bool IsFinished = false;

                        AddParam(cmd, FlexyId, "BookingId", SqlDbType.NVarChar);
                        AddParam(cmd, Desc, "Desc", SqlDbType.NVarChar);
                        AddParam(cmd, StartTime, "StartTime", SqlDbType.NVarChar);
                        AddParam(cmd, Endtime, "Endtime", SqlDbType.NVarChar);
                        AddParam(cmd, IsFinished, "IsFinished", SqlDbType.Bit);

                        cmd.CommandText =
                            "insert into BookedActivities(BookingId, TimeDesc, StartTime, Endtime, IsFinished)" +
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

                ResetDAL();
        }
        public void InsertActivityToDb(Booking booking)
        {//Made by Eby
            List<Booking.Time> times = booking.times;
            OpenConnection(con);
                foreach (var time in times)
                {
                    if (!time.timeDesc.Equals("#"))
                    {

                        cmd.Parameters.Clear();
                        string Desc = time.timeDesc;
                        AddParam(cmd, Desc, "Desc", SqlDbType.NVarChar);

                        cmd.CommandText = "insert into Activities (Description)" +
                                          " values (@Desc)";
                        try
                        {
                            cmd.ExecuteNonQuery();
                            //MessageBox.Show("Record added Successfully!");
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }

                ResetDAL();
        }
        public void InsertBookedProductsToDb(Booking booking)
        {
            List<Booking.Product> products = booking.products;
            //Made by Eby
            OpenConnection(con);
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
                        cmd.CommandText =
                            "insert into BookedProducts (BookingId, ProductDesc, productPrice, productTotPrice, productAmount)" +
                            " values (@BookingId, @ProductDesc, @productPrice, @productTotPrice, @productAmount)";
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

                ResetDAL();
            }
    }
}
