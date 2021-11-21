using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string address, string subject, string htmlMessage);
    }
}