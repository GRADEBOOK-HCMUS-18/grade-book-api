using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class ClassService : IClassService
    {
        private readonly IBaseRepository<Class> _classRepository;
        private readonly IBaseRepository<ClassStudents> _classStudentsRepository;
        private readonly ILogger<ClassService> _logger;
        private readonly IBaseRepository<User> _userRepository;

        public ClassService(ILogger<ClassService> logger,
            IBaseRepository<Class> classRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<ClassStudents> classStudentsRepository)
        {
            _logger = logger;
            _classRepository = classRepository;
            _userRepository = userRepository;
            _classStudentsRepository = classStudentsRepository;
        }

        public Class GetClassDetail(int classId)
        {
            throw new NotImplementedException();
        }

        public List<Class> GetAllClassWithUserBeingMainTeacher(int userId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            if (foundUser is null)
                throw new ApplicationException("User does not exists");

            var mainTeacherClasses =
                _classRepository.List(cl => cl.MainTeacher == foundUser, null, "MainTeacher");
            return mainTeacherClasses.ToList();
        }

        public List<Class> GetAllClassWithUserBeingSubTeacher(int userId)
        {
            var foundUser =
                _userRepository.GetFirst(user => user.Id == userId, 
                    user => user.Include(u => u.ClassTeachers).ThenInclude(c => c.Class));


            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            return foundUser.ClassTeachers.Select(ct => ct.Class).ToList();

        }

        public List<Class> GetAllClassWithUserBeingStudent(int userId)
        {
            var foundUser =
                _userRepository.GetFirst(user => user.Id == userId, 
                    user => user.Include(u => u.ClassStudents).ThenInclude(c => c.Class));


            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            return foundUser.ClassStudents.Select(ct => ct.Class).ToList();
        }

        public Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == mainTeacherId);
            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            var newClass = new Class
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
            var foundUser = _userRepository.GetFirst(user => user.Id == studentId,
                user => user.Include(u => u.ClassStudents).Include(u => u.ClassTeachers));
            if (foundUser is null)
                throw new ApplicationException("User does not exist");
            var foundClass = _classRepository.GetFirst(cl => cl.Id == classId, cl => cl.Include(c => c.MainTeacher));
            if (foundClass.MainTeacher == foundUser)
                throw new ApplicationException("User is currently the main teacher of this class");
            if (foundClass is null)
                throw new ApplicationException("Class does not exists");

            if (foundUser.ClassStudents.FirstOrDefault(c => c.ClassId == classId) is not null)
                throw new ApplicationException("User already a student in class");

            if (foundUser.ClassTeachers.FirstOrDefault(c => c.ClassId == classId) is not null)
                throw new ApplicationException("User is currently a teacher in class");


            var newClassStudentRecord = new ClassStudents
            {
                Class = foundClass,
                ClassId = foundClass.Id,
                Student = foundUser,
                StudentId = foundUser.Id
            };

            _classStudentsRepository.Insert(newClassStudentRecord);
        }

        private string GenerateRandomLetterString()
        {
            var resultGuid = string.Concat(Guid.NewGuid().ToString().Select(c => (char) (c + 17)));
            return resultGuid.Substring(resultGuid.Length - 12);
        }
    }
}