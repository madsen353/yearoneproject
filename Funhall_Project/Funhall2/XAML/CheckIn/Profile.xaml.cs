using Funhall2.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Funhall2
{
    public partial class Profile : Page
    {
        //made by Ebby
        DAL dal = new DAL();
        public Customer Customer { get; set; }
        public Booking Booking { get; set; }
        public string globalPageHistory = "";
        public Profile(Customer cus, Booking b, string pageHistory)
        {
            globalPageHistory = pageHistory;
            Customer = cus;
            Booking = b;
            InitializeComponent();
            n.Text = Booking.name;
            this.DataContext = Customer;            
            //ObservableCollection<Activity> activities = Activity.getBookedActivities(b);
            ObservableCollection<CustomerActivity> CusActivities = dal.GetCusActivities(cus);
            Activities.ItemsSource = CusActivities;
        }

        private void UpdateGuestData(object sender, RoutedEventArgs e)
        {
            //made by Ebby
            Customer cus = new Customer();
            cus.CusId = Customer.CusId;
            cus.Name = Name.Text;
            cus.Email = Email.Text;
            if (Subscription.IsChecked == true)
                cus.Subscription = true;
            else
                cus.Subscription = false;

            if (Segway.IsChecked == true)
                cus.Segway = true;
            else
                cus.Segway = false;

            try
            {
                dal.UpdateCus(cus);
                MessageBox.Show("Data er blevet opdateret");
            }
            catch (Exception)
            {
                MessageBox.Show("Der skete en fejl, prøv igen.");                
            }
            //finally
            //{
            //    SelectedBookingPage page = new SelectedBookingPage(Booking);
            //    this.NavigationService.Navigate(page);
            //}     
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            //made by Ebby
            SelectedBookingPage page = new SelectedBookingPage(Booking, globalPageHistory);
            this.NavigationService.Navigate(page);

        }
    }
}
