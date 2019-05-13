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
using Funhall2.Classes;

namespace Funhall2.XAML.PointSystem
{
    /// <summary>
    /// Interaction logic for CurrentActivity.xaml
    /// </summary>
    public partial class CurrentActivity : Page
    {
        
        public Activity activity;
        public Booking booking;
        public CurrentActivity(Activity a, Booking b)
        {
            DAL dal = new DAL();
            activity = a;
            booking = b;
            InitializeComponent();
            ObservableCollection<Customer> guests = Customer.GetCustomers(b);
            ObservableCollection<CustomerActivity> elementsToShow = new ObservableCollection<CustomerActivity>();
            int cI = 0;
            foreach (Customer guest in guests)
            {

                elementsToShow.Add(dal.getCusActivitySpecifiedByActivity(guests[cI], activity));
                cI++;
            }

            //int i = 0;
            //ObservableCollection<CustomerActivity> guestsWithActivity = new ObservableCollection<CustomerActivity>();
            //foreach (Customer guest in guests)
            //{
            //    CustomerActivity g = new CustomerActivity(guests[i], a);
            //    guestsWithActivity.Add(g);
            //    i++;
            //}
            //Guests.ItemsSource = guestsWithActivity;
            Guests.ItemsSource = elementsToShow;
        }

        private void UpdateScore(object sender, RoutedEventArgs e)
        {
            CustomerActivity c = Guests.SelectedItem as CustomerActivity;
            PointPage pointPage = new PointPage(c,booking,activity);
            this.NavigationService.Navigate(pointPage);
        }

        private void EndActivity(object sender, RoutedEventArgs e)
        {
            DAL dal = new DAL();
            activity.IsFinished = 1;
            dal.EndActivity(activity.BookingId, activity.IsFinished, activity.TimeDesc);
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }
    }

}
