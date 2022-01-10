using System;
using ApplicationCore.Entity;
using grade_book_api.Responses.Class;

namespace grade_book_api.Responses.Notification
{
    public class NotificationResponse
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        
        public AssignmentInformationResponse Assignment { get; set; }
        
        public ClassMinimumInformationResponse Class { get; set; }
        
        public string NotificationType { get; set; }

        public GradeReviewMinimumInformationResponse Review
        {
            get; set;
        }
        
        public bool IsViewed { get; set; }

        public NotificationResponse(UserNotification source)
        {
            Id = source.Id;
            DateTime = source.DateTime;
            IsViewed = source.IsViewed;
            NotificationType = source.NotificationType switch
            {
                ApplicationCore.Entity.NotificationType.NewFinalizedGradeComposition => "NewFinalizedGradeComposition",
                ApplicationCore.Entity.NotificationType.NewGradeReviewRequest => "NewGradeReviewRequest",
                ApplicationCore.Entity.NotificationType.NewGradeReviewReply => "NewGradeReviewReply",
                ApplicationCore.Entity.NotificationType.AcceptedOrRejectedGradeReview => "AcceptedOrRejectedGradeReview"
            };
            if (source.Assignment is not null)
            {
                 Assignment = new AssignmentInformationResponse(source.Assignment);
            }

            if (source.Class is not null)
            {
                Class = new ClassMinimumInformationResponse(source.Class);
            }

            if (source.AssignmentGradeReviewRequest is not null)
            {
                Review = new GradeReviewMinimumInformationResponse(source.AssignmentGradeReviewRequest);
            } 
            

           
            
        }
    }
}