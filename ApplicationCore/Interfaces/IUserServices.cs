using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices
    {
        public User GetUserById(int id);
        public User GetUserByNameOrEmail(string usernameOrEmail);

        public User UpdateUser(int id, string newFirstname, string newLastname, string newStudentIdentification,
            string newPassword, string newEmail);
    }
}