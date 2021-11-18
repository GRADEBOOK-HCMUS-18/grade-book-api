using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class UserRegisterRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [MaxLength(32)] public string FirstName { get; set; }

        [MaxLength(32)] public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }
        public string DefaultProfilePictureHex { get; set; }
    }
}