using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string UsernameOrEmail { get; set;  }
        [Required]
        public string Password { get; set; } 
    }
}