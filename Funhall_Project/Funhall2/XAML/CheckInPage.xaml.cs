using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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
            try
            {
                BookingFetcher bookingFetcher = new BookingFetcher();
                List<Booking> bookings = bookingFetcher.GetBookingsFromRemoteServer();
                bookingFetcher.InsertBookingsToDB(bookings);

                //Made by Anders & Niels
                AllBookingsPage AllBookings = new AllBookingsPage();
                this.NavigationService.Navigate(AllBookings);
            }
            catch (Exception exception)
            {
                ExceptionWriter.SaveErrorFile(exception);
                StatusText.Text = "Der kunne ikke oprettes forbindelse til fjernserveren \n eller databasen, kontakt personalet \n eller prøv igen";
                return;
            }
        }
        
        private void point_Click(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            try
            {
                ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
                this.NavigationService.Navigate(chooseYourBooking);
            }
            catch (Exception exception)
            {
                ExceptionWriter.SaveErrorFile(exception);
                StatusText.Text = "Der kunne ikke oprettes forbindelse til fjernserveren \n eller databasen, kontakt personalet \n eller prøv igen";
                return;
            }
        }
    }
}
