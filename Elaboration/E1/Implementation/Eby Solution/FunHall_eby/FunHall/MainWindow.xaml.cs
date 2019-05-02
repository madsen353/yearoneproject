using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FunHall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            using (var sftp = new SftpClient("10.152.120.24", "funhall", "ubuntu123"))
            {
                sftp.Connect();
                MessageBox.Show("connected");
                var files = sftp.ListDirectory(@"bookings/");//SFTP folder from where the file is to be download
               
                //Save all files in remoteserver to the local folder FlexyBox

                foreach (var file in files)
                {                    
                    if (file.Name.EndsWith(".json"))
                    {
                        string remoteFileName = file.Name;

                        using (Stream file1 = File.Create(@"FlexyBox/" + remoteFileName))
                        {
                            sftp.DownloadFile(@"bookings/" + remoteFileName, file1);
                        }
                    }                    
                }
                sftp.Disconnect();
            }

            //Get all files in the folder 'FlexyBox'
            string[] Files = Directory.GetFiles(@"FlexyBox/", "*.json");

           //Deserialize each file and save data to DB
            foreach (string file in Files)
            {

                Booking booking = JsonConvert.DeserializeObject<Booking>(File.ReadAllText(file),
                                   new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});

                booking.InsertBookingToDb(); //Save data to Db
            }

            InitializeComponent();
        }
    }
}
