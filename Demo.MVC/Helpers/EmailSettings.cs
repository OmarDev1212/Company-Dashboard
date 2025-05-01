using System.Net;
using System.Net.Mail;

namespace Demo.MVC.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;    
            client.Credentials = new NetworkCredential("om728526@gmail.com", "wloilcxtryuhbncq");
            client.Send("om728526@gmail.com",email.To,email.Subject,email.Body);

        }
    }
}
