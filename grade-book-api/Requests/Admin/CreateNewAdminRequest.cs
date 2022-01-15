using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests.Admin
{
    public class CreateNewAdminRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsSuperAdmin { get; set; }
    }
}