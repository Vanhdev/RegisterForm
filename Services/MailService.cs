using RegisterForm.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using RegisterForm.Configs;

namespace RegisterForm.Services
{
    public interface IMailService
    {
        Task SendRegisterMail(string fromEmail, string toEmail);
    }
    public class MailService : IMailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        public MailService(IOptions<MailConfig> mailConfig) { 
            _host = mailConfig.Value.Host;
            _port = mailConfig.Value.Port;
            _userName = mailConfig.Value.UserName;
            _password = mailConfig.Value.Password;
        }
        public async Task SendRegisterMail(string fromEmail, string toEmail)
        {
            var smtpClient = new SmtpClient(_host)
            {
                Port = _port,
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = true,
            };

            smtpClient.Send(fromEmail, toEmail, "Confirm Register", "Thank you for registration");
        }
    }
}
