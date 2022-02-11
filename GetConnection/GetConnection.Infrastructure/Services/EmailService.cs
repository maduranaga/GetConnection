using GetConnection.Core.Entities;
using GetConnection.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {

            _logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.outlook.com");
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("info@mankiwwa.lk", "mankiwwa@2020");
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("info@mankiwwa.lk");
                mailMessage.To.Add(email.To);
                mailMessage.Body = email.Content;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = email.Subject;
                client.SendAsync(mailMessage, "");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }
    }
}