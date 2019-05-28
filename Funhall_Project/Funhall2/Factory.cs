using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2
{
    public static class Factory
    {
        public static IDAL CreateDAL()
        {
            return new DAL();
        }

        public static IActivity CreateActivity()
        {
            return new Activity();
        }

        public static ICustomer CreateCustomer()
        {
            return new Customer();
        }
    }
}
