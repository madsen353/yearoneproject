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
    public class Customer
    {
        public string BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Subscription { get; set; }
        public bool Segway { get; set; }

        public static ObservableCollection<Customer> GetCustomers(Booking booking)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                 + "Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Guests";
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Customer c = new Customer();
                c.Name = reader[2].ToString(); ;
                c.Email = reader[3].ToString();
                c.Segway = reader.GetBoolean(4);
                c.Subscription = reader.GetBoolean(5);
                customers.Add(c);
            }
            con.Close();
            return customers;
        }
    }
}
