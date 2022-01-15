using SharedKernel;

namespace ApplicationCore.Entity
{
    public class AdminAccount: BaseEntity
    {
        
        
        public string Username { get; set; }
        public bool IsSuperAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public AdminAccount(string username,  string password, bool isSuperAdmin)
        {
            Username = username;
            IsSuperAdmin = isSuperAdmin;
            PasswordHelper.HashPassword(password,out var newPasswordSalt, out var newPasswordHash );

            PasswordHash = newPasswordHash;
            PasswordSalt = newPasswordSalt; 
        }
        
        public AdminAccount(){}
    }
}