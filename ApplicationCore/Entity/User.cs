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
        public IList<ClassStudents> ClassStudents { get; set; } = new List<ClassStudents>();
        public IList<ClassTeachers> ClassTeachers { get; set; } = new List<ClassTeachers>();
    }
}