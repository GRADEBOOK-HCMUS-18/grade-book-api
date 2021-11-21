using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class UserUpdateRequest
    {
        [EmailAddress] public string Email { get; set; }


        [MaxLength(32)] public string FirstName { get; set; }

        [MaxLength(32)] public string LastName { get; set; }

        public string StudentIdentification { get; set; }
    }
}