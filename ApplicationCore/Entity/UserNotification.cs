using System;

namespace ApplicationCore.Entity
{
    public enum NotificationType
    {
        NewFinalizedGradeComposition = 1,
        NewGradeReviewRequest,
        NewGradeReviewReply,
        AcceptedOrRejectedGradeReview
    }
    public class UserNotification: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int ClassId { get; set; }
        public Class Class { get; set; }
        
        public NotificationType NotificationType { get; set; }
        public int? AssignmentId { get; set; }
        public int? ReviewId { get; set; }

        public string Content { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}