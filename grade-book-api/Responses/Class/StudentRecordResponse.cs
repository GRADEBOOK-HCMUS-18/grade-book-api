using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class StudentRecordResponse
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }

        public StudentRecordResponse(StudentRecord source)
        {
            StudentId = source.StudentIdentification;
            FullName = source.FullName;
        }
    }
}