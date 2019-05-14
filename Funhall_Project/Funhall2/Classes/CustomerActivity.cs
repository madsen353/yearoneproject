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
    public class CustomerActivity
    {
        //Made by Eby
        public Customer Customer { get; set; }
        public Activity  Activity { get; set; }
        public string  Points { get; set; }
        public CustomerActivity()
        {
            Customer = new Customer();
            Activity = new Activity();
        }
        public string Name
        {
            get
            {
                return Customer.Name;
            }
        }
        public override string ToString()
        {
            return Customer.Name;
        }
        public CustomerActivity(Customer c, Activity a)
        {
            this.Activity = a;
            this.Customer = c;
        }

        public static void AddActivities(Customer cus)
        {
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            //cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;       

            cmd.CommandText = "set IDENTITY_INSERT GuestActivities ON " +
                "INSERT INTO GuestActivities (GuestId, TimeDesc, Points) "+
                                "SELECT  g.GuestId, b.TimeDesc, '0'" +
                                "FROM BookedActivities b  " +
                                "inner JOIN Guests g on  g.BookingId = b.BookingId " +
                                "where g.Email=@Email";    

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public static ObservableCollection<CustomerActivity> getCusActivities(Customer cus)
        {
            //Made by Eby
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +                
                "where ga.GuestId=@Id"; 
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CustomerActivity ca = new CustomerActivity();
               
                
                ca.Customer.CusId = (int)reader[0];
                ca.Activity.TimeDesc = reader[1].ToString();
                ca.Points = reader[2].ToString();
                //a.StartTime = DateTime.Parse(reader[2].ToString());
                //a.EndTime = DateTime.Parse(reader[3].ToString());
                cusActivities.Add(ca);
            }
            con.Close();
            return cusActivities;
        }
    }
}
