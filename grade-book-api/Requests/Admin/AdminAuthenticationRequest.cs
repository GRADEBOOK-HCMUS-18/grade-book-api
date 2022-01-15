using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests.Admin
{
    public class AdminAuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}