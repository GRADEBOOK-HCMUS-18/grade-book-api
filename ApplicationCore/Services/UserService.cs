using System.IO;
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
        private readonly ICloudPhotoHandler _cloudPhotoHandler;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository, ICloudPhotoHandler cloudPhotoHandler)
        {
            _logger = logger;
            _userRepository = userRepository;
            _cloudPhotoHandler = cloudPhotoHandler;
        }

        public User GetUserById(int id)
        {
            var found = _userRepository.GetFirst(user =>
                user.Id == id);

            if (found is null)
                return null;

            return found;
        }

        public User GetUserByNameOrEmail(string email)
        {
            var found = _userRepository.GetFirst(user =>
                user.Email == email );

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

        public string UpdateUserAvatar(int id, Stream newPicture)
        {
            var found = _userRepository.GetFirst(user => user.Id == id);
            if (found is null)
                return null;
            var resultUrl = _cloudPhotoHandler.Upload(newPicture);

            if (string.IsNullOrEmpty(resultUrl))
            {
                return null;
            }

            found.ProfilePictureUrl = resultUrl;
            _userRepository.Update(found);

            return resultUrl;
        }
    }
}