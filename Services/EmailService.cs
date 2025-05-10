using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BTicTacToe.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendResultEmail(string to, string subject, string body)
        {
            var smtpConfig = _config.GetSection("Smtp");

            var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]))
            {
                Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]),
                EnableSsl = true
            };

            var message = new MailMessage(smtpConfig["Username"], to, subject, body);
            client.Send(message);
        }
    }
}
