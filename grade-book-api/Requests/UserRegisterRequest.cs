using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [MinLength(8)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set;  }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}