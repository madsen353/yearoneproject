using System;
using System.Collections.Generic;
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
using Funhall2.XAML.PointSystem;

namespace Funhall2
{
    /// <summary>
    /// Interaction logic for CheckInPage.xaml
    /// </summary>
    public partial class CheckInPage : Page
    {
        public CheckInPage()
        {
            InitializeComponent();
        }

        private void checkIn_Click(object sender, RoutedEventArgs e)
        {
            //Made by Anders & Niels
            AllBookingsPage AllBookings = new AllBookingsPage();
            this.NavigationService.Navigate(AllBookings);
        }

        private void point_Click(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
            this.NavigationService.Navigate(chooseYourBooking);
        }
    }
}
