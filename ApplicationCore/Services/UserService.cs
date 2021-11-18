using System;
using System.IO;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace ApplicationCore.Services
{
    public class UserService : IUserServices
    {
        private readonly ICloudPhotoHandler _cloudPhotoHandler;
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Class> _classRepository;   

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository,
            ICloudPhotoHandler cloudPhotoHandler, IBaseRepository<Class> classRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _cloudPhotoHandler = cloudPhotoHandler;
            _classRepository = classRepository;
        }

        public User GetUserById(int id)
        {
            var found = _userRepository.GetFirst(user =>
                user.Id == id);

            if (found is null)
                return null;

            return found;
        }

        public User GetUserByUsername(string email)
        {
            var found = _userRepository.GetFirst(user =>
                user.Email == email);

            if (found is null)
                return null;

            return found;
        }

        public bool IsUserTeacherInClass(int userId, int classId)
        {
            var foundClass =
                _classRepository
                    .GetFirst(cl => cl.Id == classId, 
                        cl => cl.Include(c => c.MainTeacher));
            var foundUser = _userRepository.GetFirst(user => user.Id == userId,
                user => user.Include(u => u.ClassStudents)
                    .Include(u => u.ClassTeachers));
            
            if(foundClass.MainTeacher == foundUser)
            {
                return true;
            }

            if (foundUser.ClassTeachers.FirstOrDefault(c => c.ClassId == classId) is not null)
                return true;
            if (foundUser.ClassStudents.FirstOrDefault(c => c.ClassId == classId) is not null)
                return false;
            throw new ApplicationException("User is not a member of this class");



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
                PasswordHelper.HashPassword(newPassword, out var newSalt, out var newHash);
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

            if (string.IsNullOrEmpty(resultUrl)) return null;

            found.ProfilePictureUrl = resultUrl;
            _userRepository.Update(found);

            return resultUrl;
        }
    }
}