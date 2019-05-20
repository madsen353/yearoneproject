using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2.Classes
{
    public class Printer
    {
        //Made by Rasmus
        public static void Print(string fileLocation)
        {
            //Made by Rasmus
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                FileName = fileLocation
            };
            p.Start();
        }
    }
}
