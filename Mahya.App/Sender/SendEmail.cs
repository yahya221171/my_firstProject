using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;


namespace Mahya.App.Sender
{
    public class SendEmail
    {
        public static Task Send(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("nourizadehyahya@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nourizadehyahya@gmail.com", "zjlpzygnpcppxpnr");//securesally
            
            //<< بجای
            // x
            // ها، پسوردی که خود گوگل برای اپلیکیشنتون ساخته رو میزارید
            SmtpServer.EnableSsl = true; // only for port 465
            SmtpServer.Send(mail);
            return Task.CompletedTask;

        }
    }
}