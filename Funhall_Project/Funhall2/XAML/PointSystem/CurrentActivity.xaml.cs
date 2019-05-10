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
    /// Interaction logic for CurrentActivity.xaml
    /// </summary>
    public partial class CurrentActivity : Page
    {
        public CurrentActivity(Activity a, Booking b)
        {
            InitializeComponent();
            ObservableCollection<Customer> guests = Customer.GetCustomers(b);
            Guests.ItemsSource = guests;
        }

        private void UpdateScore(object sender, RoutedEventArgs e)
        {

        }
    }
}
