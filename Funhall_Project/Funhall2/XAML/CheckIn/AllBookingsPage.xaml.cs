﻿using System;
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
        DAL dal = new DAL();
        public AllBookingsPage()
        {
                //Made by Rasmus
                InitializeComponent();
                DataContext = this;
                //ListBox Solution:
                ObservableCollection<Booking> bookings = dal.getBookings();
                listBox.ItemsSource = bookings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Made by Anders & Niels
            CheckInPage checkIn = new CheckInPage();
            this.NavigationService.Navigate(checkIn);
        }
        
        private void ShowSelectedBooking(object sender, RoutedEventArgs e)
        {
            //Made by Eby
            Booking b = listBox.SelectedItem as Booking;
            if (b != null)
            {
                string currentPageName = "AllBookingsPage";
                SelectedBookingPage page = new SelectedBookingPage(b, currentPageName);
                this.NavigationService.Navigate(page); this.NavigationService.Navigate(page);

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
                    GuestCheckinPage page = new GuestCheckinPage(b);
                    this.NavigationService.Navigate(page);
                }
                else
                {
                    MessageBox.Show("Vælg en Booking fra listen");

                }

        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            CheckInPage CheckIn = new CheckInPage();
            this.NavigationService.Navigate(CheckIn);

        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            BookingFetcher bookingFetcher = new BookingFetcher();
            try
            {
                List<Booking> bookings = bookingFetcher.GetBookingsFromRemoteServer();
                bookingFetcher.InsertBookingsToDB(bookings);
            }
            catch (Exception exception)
            {
                ExceptionWriter.SaveErrorFile(exception);
                MessageBox.Show("Der kunne ikke oprettes forbindelse til fjernserveren eller databasen, kontakt personalet eller prøv igen");
                return;
            }

            //Made by Anders & Niels
            AllBookingsPage AllBookings = new AllBookingsPage();
            this.NavigationService.Navigate(AllBookings);
        }
    }
}
