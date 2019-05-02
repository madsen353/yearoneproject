using System;
using System.Collections.Generic;
using CheckInApp.ServerConfiguration;
using Newtonsoft.Json;
using Renci.SshNet;

namespace CheckInApp
{
    public class RemoteServer : IDisposable
    {
        private SftpClient client;
        private RemoteServerConfig config;

        public RemoteServer(RemoteServerConfig config)
        {
            this.config = config;
            client = new SftpClient(config.RemoteAddress, config.Username, config.Password);
        }

        public List<RawBooking> ReadAllBookings()
        {
            client.Connect();
            Console.WriteLine("Is Connected");
            var files = client.ListDirectory(config.FilePath);
            var bookings = new List<RawBooking>();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".json"))
                {
                    string remoteFileName = file.Name;
                    // This code persists the data to the local machine on a defined path
                    //using (Stream file1 = File.Create(@"Bookings\booking1.json"))
                    //{
                    //    sftp.DownloadFile(@"bookings/" + remoteFileName, file1);
                    //}

                    // This code loads the data into RAM without persisting it.
                    var json = client.ReadAllText(@"bookings/" + remoteFileName);
                    RawBooking rawBooking = JsonConvert.DeserializeObject<RawBooking>(json);
                    bookings.Add(rawBooking);
                }
            }
            client.Disconnect();
            return bookings;
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
