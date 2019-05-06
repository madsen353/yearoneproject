﻿using System;
using System.Collections.Generic;
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
            this.config = config;
            client = new SftpClient(config.RemoteAddress, config.Username, config.Password);
        }

        public List<Booking> ReadAllBookings()
        {
            client.Connect();
            Console.WriteLine("Is Connected");
            var files = client.ListDirectory(config.FilePath);//SFTP folder from where the file is to be download
            var bookings = new List<Booking>();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".json"))
                {
                    string remoteFileName = file.Name;
                    
                    // This code loads the data into RAM without persisting it.
                    var json = client.ReadAllText(@"bookings/" + remoteFileName);
                    Booking rawBooking = JsonConvert.DeserializeObject<Booking>(json);
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