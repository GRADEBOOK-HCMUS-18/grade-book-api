using System;

namespace ApplicationCore.Entity
{
    public class GradeReviewReply: BaseEntity
    {
        public User Replier { get; set; }
        public int ReplierId { get; set; }
        public AssignmentGradeReviewRequest AssignmentGradeReviewRequest { get; set; }
        public int AssignmentGradeReviewRequestId { get; set; }
        
        public string Content { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}