namespace ApplicationCore.Entity
{
    public class AdminAccount: BaseEntity
    {
        
        
        public string Username { get; set; }
        public bool IsSuperAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public AdminAccount(string username, bool isSuperAdmin)
        {
            Username = username;
            IsSuperAdmin = isSuperAdmin;
        }
        
        public AdminAccount(){}
    }
}