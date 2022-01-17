using System;
using SharedKernel;

namespace ApplicationCore.Entity
{
    public class AdminAccount: BaseEntity
    {
        
        
        public string Username { get; set; }
        public bool IsSuperAdmin { get; set; }
        
        public DateTime DateCreated { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public AdminAccount(string username,  string password, bool isSuperAdmin)
        {
            Username = username;
            IsSuperAdmin = isSuperAdmin;
            DateCreated = DateTime.Now;
            PasswordHelper.HashPassword(password,out var newPasswordSalt, out var newPasswordHash );

            PasswordHash = newPasswordHash;
            PasswordSalt = newPasswordSalt; 
        }
        public AdminAccount(int id ,string username,  string password, bool isSuperAdmin)
        {
            Id = id;
            Username = username;
            IsSuperAdmin = isSuperAdmin;
            DateCreated = DateTime.Now;
            PasswordHelper.HashPassword(password,out var newPasswordSalt, out var newPasswordHash );

            PasswordHash = newPasswordHash;
            PasswordSalt = newPasswordSalt; 
        }
        
        public AdminAccount(){}
    }
}