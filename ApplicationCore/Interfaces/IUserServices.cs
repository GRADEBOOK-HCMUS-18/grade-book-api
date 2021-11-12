using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IUserServices
    {
        public User GetUserById(int id);
        public  User GetUserByNameOrEmail(string usernameOrEmail);
    }
}