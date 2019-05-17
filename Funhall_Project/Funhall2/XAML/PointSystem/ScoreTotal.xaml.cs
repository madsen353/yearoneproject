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
    /// //Made by Rasmus
    public partial class ScoreTotal : Page
    {
        //Made by Rasmus
        public Booking booking;
        DAL dal = new DAL();
        public ScoreTotal(Booking b)
        {
            //Made by Rasmus
            booking = b;
            InitializeComponent();
            ObservableCollection<Customer> elementsToShow = dal.GetCustomers(b);
            int i = 0;
            foreach (Customer guest in elementsToShow)
            {
                elementsToShow[i].TotalAmountOfPoints = elementsToShow[i].GetTotalAmountOfPoints();
                i++;
            }
            Guests.ItemsSource = elementsToShow;
            }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            AllActivities allActivities = new AllActivities(booking);
            this.NavigationService.Navigate(allActivities);
        }
    }
}
