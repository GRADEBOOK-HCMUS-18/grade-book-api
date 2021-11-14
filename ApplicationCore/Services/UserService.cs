using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace ApplicationCore.Services
{
    public class UserService : IUserServices
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public User GetUserById(int id)
        {
            var found = _userRepository.GetFirst(user =>
                user.Id == id);

            if (found is null)
                return null;

            return found;
        }

        public User GetUserByNameOrEmail(string usernameOrEmail)
        {
            var found = _userRepository.GetFirst(user =>
                user.Email == usernameOrEmail || user.Username == usernameOrEmail);

            if (found is null)
                return null;

            return found;
        }

        public User UpdateUser(int id, string newFirstname, string newLastname, string newStudentIdentification,
            string newPassword, string newEmail)
        {
            var found = _userRepository.GetFirst(user => user.Id == id);
            if (found is null) return null;

            if (!string.IsNullOrEmpty(newFirstname)) found.FirstName = newFirstname;
            if (!string.IsNullOrEmpty(newLastname)) found.LastName = newLastname;

            if (!string.IsNullOrEmpty(newStudentIdentification)) found.StudentIdentification = newStudentIdentification;

            if (!string.IsNullOrEmpty(newEmail)) found.Email = newEmail;

            if (!string.IsNullOrEmpty(newPassword))
            {
                PasswordHelper.HashPassword(newPassword,out var newSalt, out var newHash);
                found.PasswordHash = newHash;
                found.PasswordSalt = newSalt;
            }

            var result = _userRepository.Update(found);
            return result;
        }
    }
}