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

namespace Funhall2
{
    public partial class GuestCheckinPage : Page
    {
        public Booking Booking { get; set; }

        public GuestCheckinPage(Booking booking)
        {
            Booking = booking;
            InitializeComponent();
            n.Text = Booking.name;

        }
    }
}
