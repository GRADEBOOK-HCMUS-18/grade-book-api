namespace ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        bool sendEmail(string address, string subject, string HtmlMessage);
    }
}