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
        public Assignment Assignment { get; set; }
        
        public AssignmentGradeReviewRequest AssignmentGradeReviewRequest { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public bool IsViewed { get; set; } = false;
    }
}