using System.Collections.Generic;

namespace grade_book_api.Requests.ClassRequests
{
    public class UpdateAssignmentPriorityRequest
    {
        public List<int> AssignmentIdPriorityOrder { get; set; } = new();
    }
}