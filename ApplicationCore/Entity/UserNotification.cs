using System;

namespace ApplicationCore.Entity
{
    public class UserNotification: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}