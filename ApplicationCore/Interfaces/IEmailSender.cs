using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string address, string subject, string htmlMessage);

        Task BulkSendEmail(List<string> addresses, string subject, string htmlMessage);
    }
}