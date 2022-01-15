using ApplicationCore.Entity;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Admin
{
    public class AdminAccountResponse
    {
        public int Id { get; set; }
        public bool IsSuperAdmin { get; set; }
        
        public string Username { get; set; }

        public AdminAccountResponse(AdminAccount account)
        {
            Id = account.Id;
            IsSuperAdmin = account.IsSuperAdmin;
            Username = account.Username;
        }
        
    }
}