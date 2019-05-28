using Funhall2.Classes;
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
using Funhall2.XAML.CheckIn;

namespace Funhall2
{
    public partial class GuestCheckinPage : Page
    {
        public Booking Booking { get; set; }

        public GuestCheckinPage(Booking booking)
        {
            //Made by Eby
            Booking = booking;
            InitializeComponent();
            n.Text = Booking.name;

        }

        private void AddGuestToDb(object sender, RoutedEventArgs e)
        {
            //DAL dal = new DAL();
            //Made by Eby
            IDAL dal = Factory.CreateDAL();
            //Customer cus = new Customer();
            ICustomer cus = Factory.CreateCustomer();
            cus.BookingId = Booking.flexyId;
            if (InputValidation.ValidateName(Name.Text))
            {
                cus.Name = Name.Text;

                if (InputValidation.ValidateEmail(Email.Text))
                {
                    cus.Email = Email.Text;

                    if (Subscription.IsChecked == true)
                        cus.Subscription = true;
                    else
                            cus.Subscription = false;
                    if (Segway.IsChecked == true)
                        cus.Segway = true;
                    else
                        cus.Segway = false;

                    try
                    {
                        dal.CheckInCus(cus);
                        dal.AddActivities(cus);
                        MessageBox.Show("Du er checked in");
                        AllBookingsPage AllBookings = new AllBookingsPage();
                        this.NavigationService.Navigate(AllBookings);

                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        MessageBox.Show("Der skete en fejl, prøv igen.");
                        // MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////Made by Anders & Niels
            SegwayDisclaimer segway = new SegwayDisclaimer();
            segway.ShowDialog();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            AllBookingsPage page = new AllBookingsPage();
            this.NavigationService.Navigate(page);

        }
    }
}
