using System;
using System.Collections.Generic;
using SharedKernel;

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
        public bool IsEmailConfirmed { get; set; }
        
        public DateTime DateCreated { get; set; }
        public IList<ClassStudentsAccount> ClassStudentsAccounts { get; set; } = new List<ClassStudentsAccount>();
        public IList<ClassTeachersAccount> ClassTeachersAccounts { get; set; } = new List<ClassTeachersAccount>();

        public IList<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

        public IList<GradeReviewReply> GradeReviewReplies { get; set; } = new List<GradeReviewReply>();

        public IList<AccountConfirmationRequest> AccountConfirmationRequests { get; set; } =
            new List<AccountConfirmationRequest>();

        public IList<PasswordChangeRequest> PasswordChangeRequests { get; set; } = new List<PasswordChangeRequest>();

        public void SetLockAccount(bool newState)
        {
            IsLocked = newState;
        }

        public void SetAllNotificationRead(bool newState)
        {
            foreach (var notification in UserNotifications)
            {
                notification.IsViewed = newState;
            }
        }

        public User(string email, string firstName, string lastName, string password, string profilePictureUrl,
            string defaultProfilePictureHex)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsPasswordNotSet = string.IsNullOrEmpty(password);
            ProfilePictureUrl = profilePictureUrl;
            DefaultProfilePictureHex = defaultProfilePictureHex;
            PasswordHelper.HashPassword(password, out var newPasswordSalt, out var newPasswordHash);
            PasswordSalt = newPasswordSalt;
            PasswordHash = newPasswordHash;
            DateCreated = DateTime.Now;
        }
        
        public User(){}
    }
}