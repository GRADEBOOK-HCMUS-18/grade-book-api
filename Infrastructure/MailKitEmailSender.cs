using ApplicationCore.Interfaces;
using MimeKit;

namespace Infrastructure
{
    public class MailKitEmailSender : IEmailSender
    {
        public bool sendEmail(string address, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress("An", "An");
            email.To.Add(MailboxAddress.Parse("to@gmail.com"));
            email.Subject = subject;

            var builder = new BodyBuilder {HtmlBody = "Content"};
            email.Body = builder.ToMessageBody();
            return false;
        }
    }
}