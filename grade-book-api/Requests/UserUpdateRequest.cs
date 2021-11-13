using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace grade_book_api.Requests
{
    public class UserUpdateRequest
    {
        
        [MinLength(8)]
        public string Username { get; set; }
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

    }
}