using System;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class AccountConfirmationRequest: BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }
        
        public DateTime DateTime { get; set; }

        public string ConfirmationCode { get; set; }
        
        public bool IsFinished { get; set; }
        
        public AccountConfirmationRequest(){}

        public AccountConfirmationRequest(User user)
        {
            this.User = user;
            this.UserId = user.Id;
            IsFinished = false;
            ConfirmationCode = GenerateRandomLetterString();
            DateTime = DateTime.Now;    
        }
        
        private string GenerateRandomLetterString()
        {
            var resultGuid = string.Concat(Guid.NewGuid().ToString().Select(c => (char) (c + 17)));
            return resultGuid.Substring(resultGuid.Length - 12);
        }
    }
}