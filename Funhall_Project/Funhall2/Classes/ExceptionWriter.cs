using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2.Classes
{
    public class ExceptionWriter
    {
        public static void SaveErrorFile(Exception exceptionData)
        {
            string filePath = "Error.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (exceptionData != null)
                {
                    writer.WriteLine(exceptionData.GetType().FullName);
                    writer.WriteLine("Message : " + exceptionData.Message);
                    writer.WriteLine("StackTrace : " + exceptionData.StackTrace);

                    exceptionData = exceptionData.InnerException;
                }
            }
        }
    }
}
