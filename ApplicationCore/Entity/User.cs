using System.Collections;
using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string StudentIdentification { get; set; }
        public IList<ClassStudents> ClassStudents { get; set; }
    }
}