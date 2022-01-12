using System;
using SharedKernel;

namespace ApplicationCore.Entity
{
    public class PasswordChangeRequest: BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }
        
        public DateTime DateTime { get; set; }

        public string ConfirmationCode { get; set; }
        
        public bool IsFinished { get; set; }
        
        
        public PasswordChangeRequest(){}

        public PasswordChangeRequest(User user)
        {
            User = user;
            UserId = user.Id;
            DateTime = DateTime.Now;
            ConfirmationCode = PasswordHelper.GenerateRandomLetterString();
            IsFinished = false;
        }
        
        
    }
}