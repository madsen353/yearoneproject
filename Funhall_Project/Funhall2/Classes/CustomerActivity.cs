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
    public class CustomerActivity //Made by Eby
    {        
        public Customer Customer { get; set; }
        public Activity  Activity { get; set; }
        public string  Points { get; set; }

        public CustomerActivity()
        {
            Customer = new Customer();
            Activity = new Activity();
        }

        // made by Rasmus
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

        DBConnection dBConnection = new DBConnection();
        SqlCommand cmd = new SqlCommand();

        public  void AddActivities(Customer cus) //Made by Eby
        {
            cmd.Connection = dBConnection.con;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;
            //cmd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = cus.BookingId;       

            cmd.CommandText = "set IDENTITY_INSERT GuestActivities ON " +
                "INSERT INTO GuestActivities (GuestId, TimeDesc, Points) "+
                                "SELECT  g.GuestId, b.TimeDesc, '0'" +
                                "FROM BookedActivities b  " +
                                "inner JOIN Guests g on  g.BookingId = b.BookingId " +
                                "where g.Email=@Email";    

            cmd.ExecuteNonQuery();
            dBConnection.ConnectionClose();

        }

        public  ObservableCollection<CustomerActivity> getCusActivities(Customer cus)
        {
            //Made by Eby
            cmd.Connection = dBConnection.con;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = cus.CusId;

            cmd.CommandText = "select ga.GuestId, ga.TimeDesc, ga.Points from GuestActivities ga " +                
                "where ga.GuestId=@Id"; 
            ObservableCollection<CustomerActivity> cusActivities = new ObservableCollection<CustomerActivity>();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CustomerActivity ca = new CustomerActivity();
               
                
                ca.Customer.CusId = (int)reader[0];
                ca.Activity.TimeDesc = reader[1].ToString();
                ca.Points = reader[2].ToString();
                //ca.StartTime = DateTime.Parse(reader[2].ToString());
                //a.EndTime = DateTime.Parse(reader[3].ToString());
                cusActivities.Add(ca);
            }
            dBConnection.ConnectionClose();
            return cusActivities;
        }
    }
}
