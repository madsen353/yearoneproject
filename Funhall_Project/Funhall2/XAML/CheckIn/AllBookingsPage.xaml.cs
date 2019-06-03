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

namespace Funhall2
{
    
    public partial class AllBookingsPage : Page
    {
        IDAL dal = Factory.CreateDAL();

        public AllBookingsPage()
        {
                //Made by Rasmus
                InitializeComponent();
                DataContext = this;
            //ListBox Solution:
            // ObservableCollection<Booking> bookings = dal.getBookings();
            // listBox.ItemsSource = bookings;

            ObservableCollection<Booking> bookings = dal.getBookings();
            listBox.ItemsSource = bookings;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Made by Anders & Niels
            try
            {
                CheckInPage checkIn = new CheckInPage();
                this.NavigationService.Navigate(checkIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
        }
        
        private void ShowSelectedBooking(object sender, RoutedEventArgs e)
        {
            //Made by Eby
            Booking b = listBox.SelectedItem as Booking;
            if (b != null)
            {
                try
                {
                    string currentPageName = "AllBookingsPage";
                    SelectedBookingPage page = new SelectedBookingPage(b, currentPageName);
                    this.NavigationService.Navigate(page); this.NavigationService.Navigate(page);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ExceptionWriter.SaveErrorFile(ex);
                }
                }
            else
            {
                MessageBox.Show("Vælg en Booking fra listen");
            }
        }
         
        private void Checkin_Click(object sender, RoutedEventArgs e)
        {
            //Made by Eby
                Booking b = listBox.SelectedItem as Booking;
                if (b != null)
                {
                    try
                    {
                        GuestCheckinPage page = new GuestCheckinPage(b);
                        this.NavigationService.Navigate(page);
                }
                    catch (Exception ex)
                    {
                    MessageBox.Show(ex.Message);
                    ExceptionWriter.SaveErrorFile(ex);
                }
                    
                }
                else
                {
                    MessageBox.Show("Vælg en Booking fra listen");
                }

        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            try
            {
                CheckInPage CheckIn = new CheckInPage();
                this.NavigationService.Navigate(CheckIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
            

        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            BookingFetcher bookingFetcher = new BookingFetcher();
            try
            {
                List<Booking> bookings = bookingFetcher.GetBookingsFromRemoteServer();
                // changes for SRP  by eby
                WriteBookingsToDB bookingItem = new WriteBookingsToDB();
                bookingItem.InsertBookingsToDB(bookings);
            }
            catch (Exception exception)
            {
                ExceptionWriter.SaveErrorFile(exception);
                MessageBox.Show("Der kunne ikke oprettes forbindelse til fjernserveren eller databasen, kontakt personalet eller prøv igen");
                return;
            }

            try
            {
                //Made by Anders & Niels
                AllBookingsPage AllBookings = new AllBookingsPage();
                this.NavigationService.Navigate(AllBookings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
            
        }
    }
}
