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

namespace Funhall2.XAML.PointSystem
{
    /// <summary>
    /// Interaction logic for AllActivities.xaml
    /// </summary>
    /// 
    public partial class AllActivities : Page
    {
        public Booking Booking { get; set; }
        public AllActivities(Booking booking)
        {
            Booking = booking;
            InitializeComponent();
            this.DataContext = Booking;

            ObservableCollection<Activity> activities = Activity.getBookedActivities(Booking);
            ObservableCollection<Activity> activitiesToShow = new ObservableCollection<Activity>();
            int i = 0;
            foreach (Activity activity in activities)
            {
                if (activities[i].IsFinished == 0)
                {
                    activitiesToShow.Add(activities[i]);
                }

                i++;
            }
            Activities.ItemsSource = activitiesToShow;

        }

        private void ShowSelectedActivity(object sender, RoutedEventArgs e)
        {
            Activity a = Activities.SelectedItem as Activity;
            CurrentActivity currentActivity = new CurrentActivity(a, Booking);
            this.NavigationService.Navigate(currentActivity);
        }


        //NEED UPDATE LOGIC!!!

        private void AllActivitiesHaveEnded(object sender, RoutedEventArgs e)
        {
            Mailer mailer = new Mailer();
            mailer.SendDiplomaEmails(Booking);
            ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
            this.NavigationService.Navigate(chooseYourBooking);
        }

        private void DisplayScoreTotal(object sender, RoutedEventArgs e)
        {
            ScoreTotal scorePage  = new ScoreTotal(Booking);
            this.NavigationService.Navigate(scorePage);
        }

        private void GoBackToOverview(object sender, RoutedEventArgs e)
        {
            ChooseYourBooking chooseYourBooking = new ChooseYourBooking();
            this.NavigationService.Navigate(chooseYourBooking);
        }
    }
}