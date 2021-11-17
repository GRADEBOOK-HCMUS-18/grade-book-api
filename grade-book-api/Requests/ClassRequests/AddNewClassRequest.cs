using System;

namespace grade_book_api.Requests.ClassRequests
{
    public class AddNewClassRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
    }
}