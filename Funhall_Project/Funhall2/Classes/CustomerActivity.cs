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
        public CustomerActivity(Customer c, Activity a)
        {
            this.Activity = a;
            this.Customer = c;
        }

        //Made by Rasmus
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
    }
}
