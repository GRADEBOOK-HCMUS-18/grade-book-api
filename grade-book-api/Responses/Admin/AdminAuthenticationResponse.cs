using ApplicationCore.Entity;

namespace grade_book_api.Responses.Admin
{
    public class AdminAuthenticationResponse
    {
        public AdminAccountResponse Admin { get; set; }

        public string Token { get; set; }

        public AdminAuthenticationResponse(AdminAccount account, string token)
        {
            Token = token;
            Admin = new AdminAccountResponse(account);
        }
    }
}