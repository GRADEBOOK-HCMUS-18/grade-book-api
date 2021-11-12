using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class UserService: IUserServices
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
            throw new System.NotImplementedException();
        }

        public User GetUserByNameOrEmail(string usernameOrEmail)
        {
            var found = _userRepository.GetFirst(user =>
                (user.Email == usernameOrEmail || user.Username == usernameOrEmail));

            if (found is null)
                return null;

            return found; 
        }
    }
}