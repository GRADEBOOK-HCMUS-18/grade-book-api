using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class UserUpdatePasswordRequest
    {
        public string OldPassword { get; set; }
        [Required] public string NewPassword { get; set; }
    }
}