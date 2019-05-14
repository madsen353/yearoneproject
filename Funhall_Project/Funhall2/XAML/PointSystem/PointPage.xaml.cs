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
using Funhall2.Classes;

namespace Funhall2.XAML.PointSystem
{
    /// <summary>
    /// Interaction logic for PointPage.xaml
    /// </summary>
    /// //Made by Rasmus
    public partial class PointPage : Page
    {
        public int guestID;
        public string activity;
        public Activity pageActivity;
        public Booking pageBooking;
        public PointPage(CustomerActivity c, Booking b, Activity a)
        {
            //Made by Rasmus
            pageBooking = b;
            pageActivity = a;
            InitializeComponent();
            Name.Text = c.Name;
            Points.Text = c.Points;
            guestID = c.Customer.CusId;
            activity = c.Activity.TimeDesc;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Made by Rasmus
            DAL dal = new DAL();
            dal.UpdateCusActivity(guestID, Points.Text,activity);
            //this.NavigationService.GoBack();
            CurrentActivity currentActivity = new CurrentActivity(pageActivity, pageBooking);
            this.NavigationService.Navigate(currentActivity);

        }
    }

}