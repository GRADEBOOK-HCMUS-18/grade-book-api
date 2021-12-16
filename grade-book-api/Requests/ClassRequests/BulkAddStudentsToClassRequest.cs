using System.Collections.Generic;

namespace grade_book_api.Requests.ClassRequests
{
    public class BulkAddStudentsToClassRequest
    {
        public class AddStudentToClassModel
        {
            public string StudentId { get; set; }
            public string FullName { get; set; }
        }

        public List<AddStudentToClassModel> Students { get; set; }
    }
}