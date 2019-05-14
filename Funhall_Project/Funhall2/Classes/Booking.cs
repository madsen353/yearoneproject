using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Funhall2
{
    //Made by Rasmus
    public class Booking
    {
        public string flexyId { get; set; }
        public string name { get; set; }
        public string cusTel { get; set; }
        public string quoteBooking { get; set; }
        public string cusTelAlt { get; set; }
        public string cusMail { get; set; }
        public string date { get; set; }
        public List<Time> times { get; set; }
        public List<Product> products { get; set; }
        public string totalPrice { get; set; }
        public string cusComment { get; set; }
        public string intComment { get; set; }
        public class Time
        {
            public string start { get; set; }
            public string timeDesc { get; set; }
            public string end { get; set; }
        }
        public class Product
        {
            public string product { get; set; }
            public string prodAmount { get; set; }
            public string prodPrice { get; set; }
            public string prodTotPrice { get; set; }
        }

        //public override string ToString()
        //{
        //    return string.Format($"{name}");
        //}

        
        public void InsertBookingToDb()
        {//Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Clear();
            if (flexyId != null && name != null && cusTel != null)
            {
                string BookingId = flexyId;
                string Name = name;
                string CusTel = cusTel;
                string CusTelAlt = cusTelAlt;
                string CusMail = cusMail;
                string Date = date;
                string TotalPrice = totalPrice;
                string CusComment = cusComment;
                string IntComment = intComment;

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

        public void InsertActivityToDb()
        {//Made by Eby
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

        public void InsertBookedActivitiesToDb()
        {
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

                    string FlexyId = flexyId;
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



        public void InsertBookedProductsToDb()
        {
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
                    string FlexyId = flexyId;
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
    }
}
