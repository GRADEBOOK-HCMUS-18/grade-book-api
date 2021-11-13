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
        [MaxLength(12)]
        [MinLength(6)]
        public string Password { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        
        [MaxLength(32)]
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}