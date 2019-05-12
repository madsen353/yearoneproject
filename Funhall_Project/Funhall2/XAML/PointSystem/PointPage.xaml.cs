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
    public partial class PointPage : Page
    {
        public int guestID;
        public string activity;
        public PointPage(CustomerActivity c)
        {
            InitializeComponent();
            Name.Text = c.Name;
            Points.Text = c.Points;
            guestID = c.Customer.CusId;
            activity = c.Activity.TimeDesc;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            
            DAL dal = new DAL();
            dal.UpdateCusActivity(guestID, Points.Text,activity);
            this.NavigationService.GoBack();

        }
    }

}