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
    /// Interaction logic for ScoreTotal.xaml
    /// </summary>
    public partial class ScoreTotal : Page
    {
        public Booking booking;
        public ScoreTotal(Booking b)
        {
            DAL dal = new DAL();
            booking = b;
            InitializeComponent();
            ObservableCollection<Customer> elementsToShow = Customer.GetCustomers(b);
            int i = 0;
            foreach (Customer guest in elementsToShow)
            {
                elementsToShow[i].TotalAmountOfPoints = elementsToShow[i].GetTotalAmountOfPoints();
                i++;
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

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }
    }
}
