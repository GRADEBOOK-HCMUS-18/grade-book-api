using System.IO;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices
    {
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public User GetUserByStudentIdentification(string studentIdentification);
        public ClassRole GetUserRoleInClass(int userId, int classId);

        public bool IsUserAdmin(int userId);

        public bool IsUserSuperAdmin(int userId);

        public User UpdateUser(int id, string newFirstname, string newLastname, string newStudentIdentification,
            string newEmail);

        public User UpdateUserPassword(int userId, string oldPassword, string newPassword);
        public User UpdateUserPassword(int userId, string newPassword);
        public string UpdateUserAvatar(int id, Stream newPicture);
    }
}