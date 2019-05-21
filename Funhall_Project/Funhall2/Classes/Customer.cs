using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public int GetTotalAmountOfPoints()
        {
            try
            {  
            //Made by Rasmus
            int guestId = CusId;
            int totalAmountOfPoints = 0;
            List<int> pointsFromEvents = new List<int>();
            DAL dal = new DAL();
            pointsFromEvents = dal.GetTotalAmountOfPoints(guestId);
            foreach (int point in pointsFromEvents)
            {
                totalAmountOfPoints += point;
            }
            return totalAmountOfPoints;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
