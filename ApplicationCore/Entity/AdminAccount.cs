namespace ApplicationCore.Entity
{
    public class AdminAccount: BaseEntity
    {
        
        public User User { get; set; }
        
        public bool IsSuperAdmin { get; set; }

        public AdminAccount(User user, bool isSuperAdmin)
        {
            User = user;
            IsSuperAdmin = isSuperAdmin;
        }
        
        public AdminAccount(){}
    }
}