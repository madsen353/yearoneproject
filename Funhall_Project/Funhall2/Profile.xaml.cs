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
        public Customer Customer { get; set; }
        public Booking Booking { get; set; }
        public Profile(Customer cus, Booking b)
        {
            Customer = cus;
            Booking = b;
            InitializeComponent();
            n.Text = Booking.name;
            this.DataContext = Customer;            
            //ObservableCollection<Activity> activities = Activity.getBookedActivities(b);
            ObservableCollection<CustomerActivity> CusActivities = CustomerActivity.getCusActivities(cus);
            Activities.ItemsSource = CusActivities;
        }

        private void UpdateGuestData(object sender, RoutedEventArgs e)
        {
            Customer cus = new Customer();
            //cus.Name = Name.Text;
            //cus.Email = Email.Text;
            //if (Subscription.IsChecked == true)
            //    cus.Subscription = true;
            //else
            //    cus.Subscription = false;

            //if (Segway.IsChecked == true)
            //    cus.Segway = true;
            //else
            //    cus.Segway = false;

            try
            {
                CheckIn.UpdateCus(cus);
                MessageBox.Show("Data er redegeret");
            }
            catch (Exception)
            {
                MessageBox.Show("Der sker noget fejl. Prøv igen");                
            }
            finally
            {
                CheckInPage page = new CheckInPage();
                this.NavigationService.Navigate(page);
            }     
        }
    }
}
