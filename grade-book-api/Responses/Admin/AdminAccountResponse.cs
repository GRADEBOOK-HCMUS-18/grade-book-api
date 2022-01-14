using ApplicationCore.Entity;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Admin
{
    public class AdminAccountResponse
    {
        public bool IsSuperAdmin { get; set; }
        public UserDetailedInformationResponse User { get; set; }

        public AdminAccountResponse(AdminAccount account)
        {
            IsSuperAdmin = account.IsSuperAdmin;
            User = new UserDetailedInformationResponse(account.User);
        }
        
    }
}