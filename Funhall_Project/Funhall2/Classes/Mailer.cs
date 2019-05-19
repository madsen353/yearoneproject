using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Funhall2.Classes
{
    //Made by Rasmus
    public class Mailer
    {
        public string SenderEmail = "mail@gamlenas.synology.me";
        public string Username = "Mail";
        public string Password = "ICanHazBurger1234!";
        public string Host = "gamlenas.synology.me";
        public SmtpClient MakeClient()
        {
            //Made by Rasmus
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = Host;
            client.Credentials = new System.Net.NetworkCredential(Username, Password);
            return client;
        }
        
        public MailMessage GenerateEmail(Customer guest)
        {
            //Made by Rasmus
            MailMessage mail = new MailMessage(SenderEmail, guest.Email);
            
            mail.Subject = "Tak fordi du besøgte Funhall Viborg";
            mail.Body = $"Hej {guest.Name} \n Vi vil gerne sige tak fordi du besøgte funhall Viborg \n Du scorede: {guest.GetTotalAmountOfPoints()} \n \n Godt klaret!";
            string diplomaPath = GenerateDiploma(guest);
            Attachment attachment = new Attachment(diplomaPath);
            mail.Attachments.Add(attachment);
            return mail;
        }

        public string GenerateDiploma(Customer guest)
        {
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

        public void SendDiplomaEmails(Booking booking)
        {
            //Made by Rasmus
            DAL dal = new DAL();
            SmtpClient client = MakeClient();
            ObservableCollection<Customer> recipients = dal.GetCustomers(booking);
            int i = 0;
            foreach (Customer guest in recipients)
            {
                MailMessage mail = GenerateEmail(recipients[i]);
                client.Send(mail);
                i++;
            }
            }

    }
}
