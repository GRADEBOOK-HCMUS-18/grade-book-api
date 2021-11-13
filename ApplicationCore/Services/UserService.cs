using System;
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
            
            var found = _userRepository.GetFirst(user =>
                (user.Id == id ));

            if (found is null)
                return null;

            return found; 
        }

        public User GetUserByNameOrEmail(string usernameOrEmail)
        {
            var found = _userRepository.GetFirst(user =>
                (user.Email == usernameOrEmail || user.Username == usernameOrEmail));

            if (found is null)
                return null;

            return found; 
        }

        public User UpdateUser( int id, string newFirstname, string newLastname, string newStudentIdentification)
        {
            var found = _userRepository.GetFirst(user => user.Id == id);
            if (found is null)
            {
                return null;
            }

            if (!String.IsNullOrEmpty(newFirstname))
            {
                found.FirstName = newFirstname;
            }
            if(!String.IsNullOrEmpty(newLastname))
            {
                found.LastName = newLastname;
            }

            if (!String.IsNullOrEmpty(newStudentIdentification))
            {
                found.StudentIdentification = newStudentIdentification;
            }

            var result = _userRepository.Update(found);
            return result;
        }
    }
}