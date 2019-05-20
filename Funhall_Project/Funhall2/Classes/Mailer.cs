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
            string diplomaPath = DiplomaMaker.GenerateDiploma(guest);
            Attachment attachment = new Attachment(diplomaPath);
            mail.Attachments.Add(attachment);
            return mail;
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
