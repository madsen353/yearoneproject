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
        public int CusId { get; set; }
        public string BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Subscription { get; set; }
        public bool Segway { get; set; }
        public int TotalAmountOfPoints { get; set; }

        public static ObservableCollection<Customer> GetCustomers(Booking booking) //Made by Eby

        {
            DAL dal = new DAL();
            return dal.GetCustomers(booking);
            
        }
        public int GetTotalAmountOfPoints()
        {
            //Made by Rasmus
            int guestId = CusId;
            int totalAmountOfPoints = 0;
            List<int> pointsFromEvents = new List<int>();
            DAL dal = new DAL();
            pointsFromEvents = dal.GetTotalAmountOfPoints(guestId);
            int i = 0;
            foreach (int point in pointsFromEvents)
            {
                totalAmountOfPoints += pointsFromEvents[i];
                i++;
            }
            return totalAmountOfPoints;
        }
    }
}
