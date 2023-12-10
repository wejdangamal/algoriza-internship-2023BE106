using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Services.Interfaces.IAdmin;

namespace Vzeeta.Services.Services.AccountService
{
    public class SendEmailService : ISendEmailService
    {
        private EmailSettings EmailSettings;
        public SendEmailService(IOptions<EmailSettings> mailSettings)
        {
            EmailSettings = mailSettings.Value;
        }
        public async Task sendEmail(string mailTo, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(EmailSettings.DisplayName, EmailSettings.Email ));
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = "Vzeeta Account Credentials";
            email.Body = new TextPart(TextFormat.Html) { Text= body };
            using var smtp = new SmtpClient();
            smtp.Connect(EmailSettings.Host,EmailSettings.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailSettings.Email, EmailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
