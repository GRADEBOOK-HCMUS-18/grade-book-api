using System.IO;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices
    {
        public User GetUserById(int id);
        public User GetUserByUsername(string email);

        public User UpdateUser(int id, string newFirstname, string newLastname, string newStudentIdentification,
            string newPassword, string newEmail);

        public string UpdateUserAvatar(int id, Stream newPicture);
    }
}