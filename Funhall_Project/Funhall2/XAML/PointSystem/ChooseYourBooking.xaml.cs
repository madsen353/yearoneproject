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

namespace Funhall2.XAML.PointSystem
{
    /// <summary>
    /// Interaction logic for ChooseYourBooking.xaml
    /// </summary>
    public partial class ChooseYourBooking : Page
    {
        public ChooseYourBooking()
        {
            //Made by Rasmus
            InitializeComponent();
            ObservableCollection<Booking> bookings = DALBooking.getBookings();
            listBox.ItemsSource = bookings;
        }

        private void ShowSelectedBooking(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            Booking b = listBox.SelectedItem as Booking;
            AllActivities allActivities = new AllActivities(b);
            this.NavigationService.Navigate(allActivities);
        }

        private void GoToFrontPage(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            CheckInPage CheckIn = new CheckInPage();
            this.NavigationService.Navigate(CheckIn);
        }
    }
}
