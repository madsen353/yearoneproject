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
    /// //Made by Rasmus
    public partial class CurrentActivity : Page
    {
        //Made by Rasmus
        public Activity activity;
        public Booking booking;
        DAL dal = new DAL();
        public CurrentActivity(Activity a, Booking b)
        {
            //Made by Rasmus
            activity = a;
            booking = b;
            InitializeComponent();
            ObservableCollection<Customer> guests = dal.GetCustomers(b);
            ObservableCollection<CustomerActivity> elementsToShow = new ObservableCollection<CustomerActivity>();
            foreach (Customer guest in guests)
            {
                elementsToShow.Add(dal.GetCusActivitySpecifiedByActivity(guest, activity));
            }
            Guests.ItemsSource = elementsToShow;
        }

        private void UpdateScore(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            CustomerActivity c = Guests.SelectedItem as CustomerActivity;
            PointPage pointPage = new PointPage(c,booking,activity);
            this.NavigationService.Navigate(pointPage);
        }

        private void EndActivity(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            activity.IsFinished = 1;
            dal.EndActivity(activity.BookingId, activity.IsFinished, activity.TimeDesc);
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }
    }

}
