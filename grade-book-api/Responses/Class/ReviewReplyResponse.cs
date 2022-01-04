using System;
using ApplicationCore.Entity;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class ReviewReplyResponse
    {
        public int Id { get; set; }
        public UserInformationResponse Replier { get; set; }
                
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public ReviewReplyResponse(GradeReviewReply source)
        {
            Id = source.Id;
            Replier = new UserInformationResponse(source.Replier);
            Content = source.Content;
            DateTime = source.DateTime;
        }
    }
}