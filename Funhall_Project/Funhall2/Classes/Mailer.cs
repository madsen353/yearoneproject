using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Funhall2.Classes
{
    public class Mailer
    {
        public string SenderEmail = "";
        public string Username = "";
        public string Password = "";
        public string Host = "";
        public SmtpClient MakeClient()
        {
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
            
            MailMessage mail = new MailMessage(SenderEmail, guest.Email);
            
            mail.Subject = "Tak fordi du besøgte Funhall Viborg";
            mail.Body = $"Hej {guest.Name} \n Vi vil gerne sige tak fordi du besøgte funhall Viborg \n Du scorede: {guest.GetTotalAmountOfPoints()} \n \n Godt klaret!";
            return mail;
        }

        public void SendDiplomaEmails(Booking booking)
        {
            SmtpClient client = MakeClient();
            ObservableCollection<Customer> recipients = Customer.GetCustomers(booking);
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
