using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure
{
    public class MailKitEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailKitEmailSender> _logger;

        public MailKitEmailSender(ILogger<MailKitEmailSender> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendEmail(string address, string subject, string htmlMessage)
        {
            var senderAddress = _configuration["MailSettings:Account"];
            var email = new MimeMessage {Sender = new MailboxAddress("GradeBook", senderAddress)};
            email.From.Add(new MailboxAddress("GradeBook", senderAddress));
            email.To.Add(MailboxAddress.Parse(address));
            email.Subject = subject;

            var builder = new BodyBuilder {HtmlBody = htmlMessage};
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            string stmpHost = _configuration["MailSettings:Host"];
            await smtp.ConnectAsync(stmpHost);
            string senderPassword = _configuration["MailSettings:Password"];
            await smtp.AuthenticateAsync(senderAddress, senderPassword);
            await smtp.SendAsync(email);


            await smtp.DisconnectAsync(true);
        }

        public Task BulkSendEmail(List<string> addresses, string subject, string htmlMessage)
        {
            foreach (var address in addresses)
            {
                SendEmail(address, subject, htmlMessage);
            }
            return Task.CompletedTask;
        }
    }
}