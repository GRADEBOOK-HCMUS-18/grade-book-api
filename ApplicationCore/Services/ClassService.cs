using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class ClassService : IClassService
    {
        private readonly ILogger<ClassService> _logger;
        private readonly IBaseRepository<Class> _classRepository;
        private readonly IBaseRepository<User> _userRepository;

        private string GenerateRandomLetterString()
        {
            var resultGuid = String.Concat(System.Guid.NewGuid().ToString().Select(c => (char) (c + 17)));
            return resultGuid.Substring(resultGuid.Length - 12); 
        }

        public ClassService(ILogger<ClassService> logger, IBaseRepository<Class> classRepository, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _classRepository = classRepository;
            _userRepository = userRepository;
        }
        public Class GetClassDetail(int classId)
        {
            throw new NotImplementedException();
        }

        public List<Class> GetAllClassWithUser(int userId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            if (foundUser is null)
                throw new ApplicationException("User does not exists");

            var mainTeacherClasses = _classRepository.List(cl => cl.MainTeacher.Id == userId, null, "MainTeacher");
            return mainTeacherClasses.ToList();
        }

        public Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == mainTeacherId);
            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            Class newClass = new Class()
            {
                Name = name,
                StartDate = startDate,
                Room = room,
                Description = description,
                MainTeacher = foundUser
            };
            newClass.InviteStringStudent = GenerateRandomLetterString();
            newClass.InviteStringTeacher = GenerateRandomLetterString();

            _classRepository.Insert(newClass);
            return newClass;
        }

        public void AddStudentToClass(int classId, int studentId)
        {
            throw new NotImplementedException();
        }
    }
}