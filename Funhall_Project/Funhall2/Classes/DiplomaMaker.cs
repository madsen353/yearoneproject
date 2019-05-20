using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string imageSavePath = "DiplomaWithData.bmp";
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
    }
}
