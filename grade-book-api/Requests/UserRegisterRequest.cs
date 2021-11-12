using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set;  }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}