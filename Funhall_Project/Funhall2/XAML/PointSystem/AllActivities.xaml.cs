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
        public Booking Booking { get; set; }
        public AllActivities(Booking booking)
        {
            //Made by Rasmus
            Booking = booking;
            InitializeComponent();
            this.DataContext = Booking;
            ObservableCollection<Activity> activities = dal.GetBookedActivities(Booking);
            ObservableCollection<Activity> activitiesToShow = new ObservableCollection<Activity>();
            foreach (Activity activity in activities)
            {
                if (activity.IsFinished == 0)
                {
                    activitiesToShow.Add(activity);
                }
            }
            Activities.ItemsSource = activitiesToShow;
            }

        private void ShowSelectedActivity(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            Activity a = Activities.SelectedItem as Activity;
            CurrentActivity currentActivity = new CurrentActivity(a, Booking);
            this.NavigationService.Navigate(currentActivity);
        }

        private void AllActivitiesHaveEnded(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            Mailer mailer = new Mailer();
            mailer.SendDiplomaEmails(Booking);
            ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
            this.NavigationService.Navigate(chooseYourBooking);
        }

        private void DisplayScoreTotal(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            ScoreTotal scorePage  = new ScoreTotal(Booking);
            this.NavigationService.Navigate(scorePage);
        }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
            this.NavigationService.Navigate(chooseYourBooking);
        }
    }
}
