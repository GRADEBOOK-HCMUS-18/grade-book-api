using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string DefaultProfilePictureHex { get; set; }
        public string StudentIdentification { get; set; }
        public bool IsPasswordNotSet { get; set; } = false;
        
        public bool IsLocked { get; set; } 
        public IList<ClassStudentsAccount> ClassStudentsAccounts { get; set; } = new List<ClassStudentsAccount>();
        public IList<ClassTeachersAccount> ClassTeachersAccounts { get; set; } = new List<ClassTeachersAccount>();

        public IList<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

        public void SetLockAccount(bool newState)
        {
            IsLocked = newState;
        }
    }
}