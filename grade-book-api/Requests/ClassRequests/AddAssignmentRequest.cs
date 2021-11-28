using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests.ClassRequests
{
    public class AddAssignmentRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Point { get; set; }
    }
}