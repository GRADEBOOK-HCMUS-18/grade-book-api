using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class AssignmentGradeResponse
    {
        
        public int StudentAssignmentGradeId { get; set; }
        public string StudentId { get; set; }
        public int Point { get; set; }
        public bool IsFinalized { get; set; }

        public AssignmentGradeResponse(StudentAssignmentGrade source)
        {
            StudentAssignmentGradeId = source.Id;
            StudentId = source.StudentRecord.StudentIdentification;
            Point = source.Point;
            IsFinalized = source.IsFinalized;
        }
    }
}