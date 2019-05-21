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

    //Exception handling in the DAL class is made by Anders & Niels
    public class DAL
    {
        //Made by Rasmus
        private SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
        private SqlCommand cmd = new SqlCommand();

        public DAL()
        {
            try
            {
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            }
            catch
            {
                throw;
            }
        }
        public void ResetDAL()
        {
            try
            {
            con.Close();
            con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
            cmd = new SqlCommand {Connection = con, CommandType = CommandType.Text};
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
            

        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            try
            {
            //Made by Rasmus
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
            }
            catch
            {
                throw;
            }
        }

        //Made by Rasmus
        public void UpdateCusActivity(int id, string points, string activityName)
        {
            try
            {
            //Made by Rasmus
            con.Open();
            cmd.CommandText = "UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName";
            cmd.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            cmd.Parameters.Add(CreateParam("@Points", points, SqlDbType.NVarChar));
            cmd.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void EndActivity(string id, int isFinished, string activityName)
        {
            try
            {
            //Made by Rasmus
            con.Open();
            cmd.CommandText = "UPDATE BookedActivities SET IsFinished = @IsFinished WHERE BookingId = @ID AND TimeDesc = @ActivityName";
            cmd.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            cmd.Parameters.Add(CreateParam("@IsFinished", isFinished, SqlDbType.Int));
            cmd.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public CustomerActivity GetCusActivitySpecifiedByActivity(Customer cus, Activity act)
        {
            try
            {
            //Made by Rasmus
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;
            cmd.Parameters.Add("@Act", SqlDbType.NVarChar).Value = act.TimeDesc;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id AND ga.TimeDesc=@Act";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            con.Open();
            }
            catch
            {
                con.Close();
                throw;
            }
           
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                CustomerActivity ca = new CustomerActivity(cus, act);
                ca.Customer.CusId = (int) reader[0];
                ca.Activity.TimeDesc = reader[1].ToString();
                ca.Points = reader[2].ToString();
                //a.StartTime = DateTime.Parse(reader[2].ToString());
                //a.EndTime = DateTime.Parse(reader[3].ToString());
                try
                {
                    ResetDAL();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return ca;
            }
        }

        public List<int> GetTotalAmountOfPoints(int guestID)
        {
            try
            {
            //Made by Rasmus
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = guestID;
            cmd.CommandText = "select Points from GuestActivities where GuestId=@Id";
            List<int> allPoints = new List<int>();
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {    
            while (reader.Read())
            {
                string pointInString = reader[0].ToString();
                int point = int.Parse(pointInString);
                allPoints.Add(point);
            }
            }    
            }
            catch
            {
                throw;
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            return allPoints;
        }

        public ObservableCollection<Activity> GetBookedActivities(Booking booking)
        {
            //Made by Eby
            cmd.Parameters.Add("@bookingId", SqlDbType.NVarChar).Value = booking.flexyId;
            cmd.CommandText = "select * from BookedActivities where BookingId = @bookingId";
            ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Activity a = new Activity();
                    a.BookingId = reader[0].ToString();
                    a.TimeDesc = reader[1].ToString();
                    a.StartTime = DateTime.Parse(reader[2].ToString());
                    a.EndTime = DateTime.Parse(reader[3].ToString());
                    a.IsFinished = (int) reader[4];
                    activities.Add(a);
                }
            }
            ResetDAL();
            return activities;
        }
        public void CheckInCus(Customer cus)
        {
            try
            {
            //Made by Eby
            cmd.CommandText = "insert into Guests (BookingId, Name, Email, AgreeTerms, Subscription) values " +
                              "(@BookingId, @Name, @Email, @AgreeTerms, @Subscription)";
            cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;
            con.Open();
            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateCus(Customer cus)
        {
            try
            {
            //Made by Eby
            cmd.CommandText = "Update Guests set  Name=@Name, Email=@Email, AgreeTerms=@AgreeTerms," +
                              "Subscription=@Subscription";
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cus.Name;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            cmd.Parameters.Add("@AgreeTerms", SqlDbType.Bit).Value = cus.Segway;
            cmd.Parameters.Add("@Subscription", SqlDbType.Bit).Value = cus.Subscription;
            con.Open();
            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddActivities(Customer cus)
        {
            try
            {
            //Made by Eby
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            //cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;       
            cmd.CommandText = "set IDENTITY_INSERT GuestActivities ON " +
                              "INSERT INTO GuestActivities (GuestId, TimeDesc, Points) " +
                              "SELECT  g.GuestId, b.TimeDesc, '0'" +
                              "FROM BookedActivities b  " +
                              "inner JOIN Guests g on  g.BookingId = b.BookingId " +
                              "where g.Email=@Email";
            con.Open();
            cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ObservableCollection<CustomerActivity> GetCusActivities(Customer cus)
        {
            //Made by Eby
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +
                              "where ga.GuestId=@Id";
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CustomerActivity ca = new CustomerActivity();
                    ca.Customer.CusId = (int) reader[0];
                    ca.Activity.TimeDesc = reader[1].ToString();
                    ca.Points = reader[2].ToString();
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
            con.Open();
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
            cmd.CommandText = "select * from Bookings";
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            con.Open();
            
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



        //REFACTOR THOSE UNDERNEATH
        // All uses the ADDPARAM method.

        public static void AddParam(SqlCommand cmd, object value, string name, SqlDbType sqlDbType)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
            

        }
        public void InsertBookingToDb(Booking booking)
        {//Made by Eby
            try
            {
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

                cmd.CommandText = "insert into Bookings (BookingId, Name, CusTel, CusTelAlt, Cusmail, Date, TotalPrice, CusComment, IntComment)" +
                    " values (@BookingId, @Name, @CusTel, @CusTelAlt, @Cusmail, @Date, @TotalPrice, @CusComment, @IntComment)";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added Successfully!");
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void InsertBookedActivitiesToDb(Booking booking)
        {
            try
            {
            List<Booking.Time> times = booking.times;
            //Made by Eby
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

                    cmd.CommandText = "insert into BookedActivities(BookingId, TimeDesc, StartTime, Endtime, IsFinished)" +
                                      "values (@BookingId, @Desc, @StartTime, @Endtime, @IsFinished)";

                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("Record added Successfully!");
                }
            }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
                ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void InsertActivityToDb(Booking booking)
        {//Made by Eby
            try
            {
            List<Booking.Time> times = booking.times;
            con.Open();

            foreach (var time in times)
            {
                if (!time.timeDesc.Equals("#"))
                {

                    cmd.Parameters.Clear();
                    string Desc = time.timeDesc;
                    AddParam(cmd, Desc, "Desc", SqlDbType.NVarChar);

                    cmd.CommandText = "insert into Activities (Description)" +
                                      " values (@Desc)";
                 cmd.ExecuteNonQuery();
                }
            }
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            try
            {
            ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void InsertBookedProductsToDb(Booking booking)
        {
            try
            {
            List<Booking.Product> products = booking.products;
            //Made by Eby
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
                        cmd.ExecuteNonQuery();
                }
            }
                    
            }
            catch (Exception)
            {
                throw;     
            }
            finally
            {
                con.Close();
            }
            try
            {
            ResetDAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
