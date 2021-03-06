﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Funhall2.Classes;

namespace Funhall2
{
    //Made by Rasmus
    public class Booking
    {
        public string flexyId { get; set; }
        public string name { get; set; }
        public string cusTel { get; set; }
        public string quoteBooking { get; set; }
        public string cusTelAlt { get; set; }
        public string cusMail { get; set; }
        public string date { get; set; }
        public List<Time> times { get; set; }
        public List<Product> products { get; set; }
        public string totalPrice { get; set; }
        public string cusComment { get; set; }
        public string intComment { get; set; }
        public class Time
        {
            public string start { get; set; }
            public string timeDesc { get; set; }
            public string end { get; set; }
        }
        public class Product
        {
            public string product { get; set; }
            public string prodAmount { get; set; }
            public string prodPrice { get; set; }
            public string prodTotPrice { get; set; }
        }
    }
}
