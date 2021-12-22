using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class AssignmentGradeResponse
    {
        
        public string StudentId { get; set; }
        public int Point { get; set; }
        public bool IsFinalized { get; set; }

        public AssignmentGradeResponse(StudentAssignmentGrade source)
        {
            StudentId = source.StudentRecord.StudentIdentification;
            Point = source.Point;
            IsFinalized = source.IsFinalized;
        }
    }
}