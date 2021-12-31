using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public class StudentAssignmentGrade : BaseEntity
    {
        public int StudentRecordId;
        public StudentRecord StudentRecord { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int Point { get; set; }

        public bool IsFinalized { get; set; }
        
        public List<AssignmentGradeReviewRequest> AssignmentGradeReviewRequests { get; set; }
    }
}