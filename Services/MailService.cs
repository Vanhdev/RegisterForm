using RegisterForm.Models;
using System.Net.Mail;
using System.Net;

namespace RegisterForm.Services
{
    public interface IMailService
    {
        Task SendRegisterMail(string fromEmail, string toEmail);
    }
    public class MailService : IMailService
    {
        public async Task SendRegisterMail(string fromEmail, string toEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vietanhtran2069@gmail.com", "ylkv rnol cwpq hhsi"),
                EnableSsl = true,
            };

            smtpClient.Send(fromEmail, toEmail, "Confirm Register", "Thank you for registration");
        }
    }
}
