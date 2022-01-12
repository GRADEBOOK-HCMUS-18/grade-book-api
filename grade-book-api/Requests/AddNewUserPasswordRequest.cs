using System.ComponentModel.DataAnnotations;

namespace grade_book_api.Requests
{
    public class AddNewUserPasswordRequest
    {
        [Required] public string NewPassword { get; set; }
    }
}