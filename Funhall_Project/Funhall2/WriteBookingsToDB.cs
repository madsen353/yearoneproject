// refactoring for SRP  by Eby
using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2
{
   public class WriteBookingsToDB
    {
        public void InsertBookingsToDB(List<Booking> _bookings)
        {
            //Made by Eby
            //Refactored by Rasmus(methods moved from Booking class to DAL class.
            IDAL dal = new DAL();
            foreach (var booking in _bookings)
            {
                dal.InsertBookingToDb(booking);
                dal.InsertBookedActivitiesToDb(booking);
                dal.InsertActivityToDb(booking);
                dal.InsertBookedProductsToDb(booking);
            }
        }
    }
}
