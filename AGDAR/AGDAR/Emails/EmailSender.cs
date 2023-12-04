using System.Net;
using System.Net.Mail;

namespace AGDAR.Emails
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message) 
        {
            var mail = "AGDARSHOP@gmail.com";
            var pw = "tfknpqycwcgrjgou";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
