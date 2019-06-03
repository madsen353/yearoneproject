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
    /// Interaction logic for AllActivities.xaml
    /// </summary>
    ///
    /// //Made by Rasmus
    public partial class AllActivities : Page
    {
        DAL dal = new DAL();

        public Booking booking;
        public AllActivities(Booking inputBooking)
        {
            //Made by Rasmus
            booking = inputBooking;
            InitializeComponent();
            this.DataContext = booking;
            ObservableCollection<Activity> activities = dal.GetBookedActivities(booking);
            ObservableCollection<Activity> activitiesToShow = new ObservableCollection<Activity>();
            foreach (Activity activity in activities)
            {
                if (!(activity.TimeDesc == "Spisning" || activity.TimeDesc == "Fri leg"))
                {

                    if (activity.IsFinished == false)
                    {
                        activitiesToShow.Add(activity);
                    }
                }
            }
            Activities.ItemsSource = activitiesToShow;
            }

        private void ShowSelectedActivity(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            Activity a = Activities.SelectedItem as Activity;
            if (a != null)
            {
                try
                {
                    CurrentActivity currentActivity = new CurrentActivity(a, booking);
                    this.NavigationService.Navigate(currentActivity);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ExceptionWriter.SaveErrorFile(ex);
                }

            }

            else
            {
                MessageBox.Show("Vælg en aktivitet fra listen");

            }
        }

        private void AllActivitiesHaveEnded(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            try
            {
                string fileToPrint = DiplomaMaker.GeneratePDF(booking);
                Printer.Print(fileToPrint);
                Mailer mailer = new Mailer();
                mailer.SendDiplomaEmails(booking);
                dal.EndBooking(booking.flexyId, true);
                ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
                this.NavigationService.Navigate(chooseYourBooking);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
            
        }

        private void DisplayScoreTotal(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            try
            {
                ScoreTotal scorePage = new ScoreTotal(booking);
                this.NavigationService.Navigate(scorePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
        }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            try
            {
                //Made by Rasmus
                ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
                this.NavigationService.Navigate(chooseYourBooking);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
        }

        private void ChangeUserData(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentPage = "AllActivities";
                SelectedBookingPage page = new SelectedBookingPage(booking, currentPage);
                this.NavigationService.Navigate(page);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ExceptionWriter.SaveErrorFile(ex);
            }
        }
    }
}
