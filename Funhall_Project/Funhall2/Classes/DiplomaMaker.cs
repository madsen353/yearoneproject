using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;

namespace Funhall2.Classes
{
    public class DiplomaMaker
    {
        //Made by Rasmus
        public static string GenerateDiploma(Customer guest)
        {
            //Made by Rasmus
            string guestName = guest.Name;
            string score = $"At score {guest.TotalAmountOfPoints} point ved Funhall";
            string date = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";
            string signature = "Funhall Viborg";
            string firstText = "Hello";
            string secondText = "World";

            PointF firstLocation = new PointF(1250f, 800f);
            PointF secondLocation = new PointF(1500f, 1025f);
            PointF thirdLocation = new PointF(625f, 1485f);
            PointF fourthLocation = new PointF(1750f, 1485f);
            //FilePaths is bin/debug
            string imageFilePath = "DiplomaTemplate.bmp";
            string imageSavePath = $"Diplomas/DiplomaWithData{guest.Name}.bmp";
            Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);//load the image file

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 25))
                {
                    graphics.DrawString(guestName, arialFont, Brushes.Black, firstLocation);
                }
                using (Font arialFont = new Font("Arial", 20))
                {
                    graphics.DrawString(score, arialFont, Brushes.DarkOrange, secondLocation);
                }
                using (Font arialFont = new Font("Arial", 20))
                {
                    graphics.DrawString(date, arialFont, Brushes.Black, thirdLocation);
                }
                using (Font arialFont = new Font("Arial", 20))
                {
                    graphics.DrawString(signature, arialFont, Brushes.Black, fourthLocation);
                }
            }

            bitmap.Save(imageSavePath);//save the image file
            return imageSavePath;
        }

        public static string GeneratePDF(Booking booking)
        {
                string savePath = "TotalScore.pdf";
                DAL dal = new DAL();
                ObservableCollection<Customer> allGuests = dal.GetCustomers(booking);
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath,FileMode.Create));
                document.Open();
                Chunk introChunk = new Chunk("Her får i jeres totale score: \n");
                document.Add(introChunk);
                foreach (Customer guest in allGuests)
                {
                    Paragraph paragraph = new Paragraph($"{guest.Name} scorede: {guest.TotalAmountOfPoints} point. \n");
                    document.Add(paragraph);
                }
                document.Close();
                return savePath;
        }
    }
}
