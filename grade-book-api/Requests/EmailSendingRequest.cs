using System.Collections.Generic;

namespace grade_book_api.Requests
{
    public class EmailSendingRequest
    {
        public List<string> MailList { get; set; } = new();
        public bool IsEmailForInvitingTeacher { get; set; }
        public string MailSubject { get; set; }
        public string UrlToSend { get; set; }
        public string MailContent { get; set; }
    }
}