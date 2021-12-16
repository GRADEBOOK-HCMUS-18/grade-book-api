using System.Collections.Generic;

namespace grade_book_api.Requests.ClassRequests
{
    public class BulkAddGradesToAssignmentRequest
    {
        public class AddGradeToAssignmentModel
        {
            public string StudentId { get; set; }
            public int Grade { get; set; }
        }
        
        public List<AddGradeToAssignmentModel> Grades { get; set; }
    }
}