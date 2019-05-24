using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Funhall2.Classes
{
    public interface IDAL
    {
        void AddActivities(ICustomer cus);
        //void CheckInCus(Customer cus);
        void EndActivity(string id, bool isFinished, string activityName);
        ObservableCollection<Activity> GetBookedActivities(Booking booking);
        ObservableCollection<Booking> getBookings();
        ObservableCollection<CustomerActivity> GetCusActivities(Customer cus);
        CustomerActivity GetCusActivitySpecifiedByActivity(Customer cus, Activity act);
        ObservableCollection<Customer> GetCustomers(Booking booking);
        List<int> GetTotalAmountOfPoints(int guestID);
        void InsertActivityToDb(Booking booking);
        void InsertBookedActivitiesToDb(Booking booking);
        void InsertBookedProductsToDb(Booking booking);
        void InsertBookingToDb(Booking booking);
        void OpenConnection(SqlConnection con);
        void ResetDAL();
        void UpdateCus(Customer cus);
        void UpdateCusActivity(int id, string points, string activityName);
        void CheckInCus(ICustomer cus);
    }
}