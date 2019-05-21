using System;
using System.Collections.Generic;
using System.Windows;
using Funhall2.Classes;
using Newtonsoft.Json;
using Renci.SshNet;

namespace Funhall2
{
    //Made by Rasmus

    public class RemoteServer : IDisposable
    {
        private SftpClient client;
        private RemoteServerConfig config;

        public RemoteServer(RemoteServerConfig config)
        {
            //Made by Rasmus
            this.config = config;
            client = new SftpClient(config.RemoteAddress, config.Username, config.Password);
        }

        private bool ConnectToClient(SftpClient client)
        {
            bool connectionStatus = true;
            try
            {
                //Thread til at vise box mens client kører videre?
                AutoClosingMessageBox.Show("Der bliver forsøgt at oprette forbindelse til serveren der indeholder dagens bookings", "Infromations Box(Kræver ikke handling)", 5000);
                client.Connect();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                connectionStatus = false;
            }
            return connectionStatus;
        }
        public List<Booking> ReadAllBookings()
        {
            //Made by Rasmus
            bool connectionStatus = ConnectToClient(client);
            var bookings = new List<Booking>();
            if (connectionStatus == true)
            {
                MessageBox.Show("Forbindelse er blevet etableret");
                var files = client.ListDirectory(config.FilePath);//SFTP folder from where the file is to be download

                foreach (var file in files)
                {
                    if (file.Name.EndsWith(".json"))
                    {
                        string remoteFileName = file.Name;

                        // This code loads the data into RAM without persisting it.
                        var json = client.ReadAllText(config.FilePath + remoteFileName);
                        Booking rawBooking = JsonConvert.DeserializeObject<Booking>(json);
                        bookings.Add(rawBooking);
                    }
                }
                client.Disconnect();
                return bookings;
            }
            else if (connectionStatus == false)
            {
                Booking bookingError = new Booking();
                bookingError.name = "ServerErrorOccured";
                bookings.Add(bookingError);
            }

            return bookings;
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }
}