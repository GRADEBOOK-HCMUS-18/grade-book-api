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
        private readonly IBaseRepository<ClassStudentsAccount> _classStudentsRepository;
        private readonly IBaseRepository<ClassTeachersAccount> _classTeacherRepository;
        private readonly IBaseRepository<User> _userRepository;

        public ClassService(
            IBaseRepository<Class> classRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<ClassStudentsAccount> classStudentsRepository,
            IBaseRepository<ClassTeachersAccount> classTeacherRepository
        )
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
            _classStudentsRepository = classStudentsRepository;
            _classTeacherRepository = classTeacherRepository;
        }

        public Class GetClassDetail(int classId)
        {
            var foundClass = _classRepository.GetFirst(cl => cl.Id == classId,
                cl => cl.Include(c => c.MainTeacher)
                    .Include(c => c.ClassStudentsAccounts).ThenInclude(cs => cs.Student)
                    .Include(c => c.ClassTeachersAccounts).ThenInclude(ct => ct.Teacher)
                    .Include(c => c.ClassAssignments));
            if (foundClass is null)
                return null;
            foundClass.ClassAssignments = foundClass.ClassAssignments.OrderBy(a => a.Priority)
                .ThenByDescending(assignment => assignment.Id)
                .ToList();

            return foundClass;
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
                    user => user.Include(u => u.ClassTeachersAccounts).ThenInclude(c => c.Class)
                        .ThenInclude(cl => cl.MainTeacher));


            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            return foundUser.ClassTeachersAccounts.Select(ct => ct.Class).ToList();
        }

        public List<Class> GetAllClassWithUserBeingStudent(int userId)
        {
            var foundUser =
                _userRepository.GetFirst(user => user.Id == userId,
                    user => user.Include(u => u.ClassStudentsAccounts).ThenInclude(c => c.Class)
                        .ThenInclude(cl => cl.MainTeacher));


            if (foundUser is null)
                throw new ApplicationException("User does not exists");
            return foundUser.ClassStudentsAccounts.Select(ct => ct.Class).ToList();
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

        public void AddStudentAccountToClass(int classId, int studentId)
        {
            var foundUser = GetUserWithClassInformation(studentId);
            if (foundUser is null)
                throw new ApplicationException("User does not exist");
            // check user already a member in class. 
            var availableClass = TryGetAvailableClass(foundUser, classId);


            var newClassStudentRecord = new ClassStudentsAccount
            {
                Class = availableClass,
                ClassId = availableClass.Id,
                Student = foundUser,
                StudentAccountId = foundUser.Id
            };

            _classStudentsRepository.Insert(newClassStudentRecord);
        }

        public void AddTeacherAccountToClass(int classId, int teacherId)
        {
            var foundUser = GetUserWithClassInformation(teacherId);
            if (foundUser is null)
                throw new ApplicationException("User does not exist");
            // check user already a member in class. 
            var availableClass = TryGetAvailableClass(foundUser, classId);


            var newClassTeacherRecord = new ClassTeachersAccount
            {
                ClassId = availableClass.Id,
                TeacherId = foundUser.Id
            };
            _classTeacherRepository.Insert(newClassTeacherRecord);
        }


        public List<StudentRecord> BulkAddStudentToClass(int classId, List<Tuple<string, string>> idNamePairs)
        {
            var foundClass = _classRepository
                .GetFirst(cl => cl.Id == classId, cl => cl.Include(c => c.Students));
            if (foundClass is null)
                return null;
            foundClass.AddStudents(idNamePairs);
            _classRepository.Update(foundClass);
            return foundClass.Students.ToList();
        }


        public List<StudentRecord> GetStudentListInClass(int classId)
        {
            var foundClass = _classRepository
                .GetFirst(cl => cl.Id == classId, cl => cl.Include(c => c.Students));
            var studentRecords = foundClass.Students.ToList();
            return studentRecords;
        }

        public StudentRecord GetStudentRecordOfUserInClass(int userId, int classId)
        {
            var studentsInClass = GetStudentListInClass(classId);
            var foundUser = _userRepository.GetFirst(u => u.Id == userId);
            var foundStudentRecord =
                studentsInClass.FirstOrDefault(s => s.StudentIdentification == foundUser.StudentIdentification);
            return foundStudentRecord;
        }

        private User GetUserWithClassInformation(int studentId)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == studentId,
                user => user.Include(u => u.ClassStudentsAccounts).Include(u => u.ClassTeachersAccounts));
            return foundUser;
        }


        private Class TryGetAvailableClass(User foundUser, int classId)
        {
            var foundClass = _classRepository.GetFirst(cl => cl.Id == classId, cl => cl.Include(c => c.MainTeacher));
            if (foundClass is null)
                throw new ApplicationException("Class does not exists");
            if (foundClass.MainTeacher == foundUser)
                throw new ApplicationException("User is currently the main teacher of this class");


            if (foundUser.ClassStudentsAccounts.FirstOrDefault(c => c.ClassId == classId) is not null)
                throw new ApplicationException("User already a student in class");

            if (foundUser.ClassTeachersAccounts.FirstOrDefault(c => c.ClassId == classId) is not null)
                throw new ApplicationException("User is currently a teacher in class");
            return foundClass;
        }

        private string GenerateRandomLetterString()
        {
            var resultGuid = string.Concat(Guid.NewGuid().ToString().Select(c => (char) (c + 17)));
            return resultGuid.Substring(resultGuid.Length - 12);
        }
    }
}