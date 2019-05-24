using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Funhall2.Classes
{
    public class CustomerActivity : ICustomerActivity
    {
        //Made by Eby
        public ICustomer Customer { get; set; }
        public IActivity  Activity { get; set; }
        public int  Points { get; set; }

        public CustomerActivity()
        {
            Customer = new Customer();
            Activity = new Activity();
            //Activity = Factory.CreateActivity();
            //Customer = Factory.CreateCustomer();
        }
        public CustomerActivity(Customer c, Activity a)
        {
            //Made by Rasmus

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
        //Made by Rasmus
        public override string ToString()
        {
            return Customer.Name;
        }
    }
}
