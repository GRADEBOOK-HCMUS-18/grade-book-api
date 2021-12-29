using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IBaseRepository<Class> _classRepository;

        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<StudentAssignmentGrade> _sGradeRepository;
        private readonly IBaseRepository<StudentRecord> _studentRecordRepository;

        public AssignmentService(
            IBaseRepository<Class> classRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<Assignment> assignmentRepository,
            IBaseRepository<StudentAssignmentGrade> sGradeRepository,
            IBaseRepository<StudentRecord> studentRecordRepository
        )
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
            _sGradeRepository = sGradeRepository;
            _studentRecordRepository = studentRecordRepository;
        }

        public List<Assignment> GetClassAssignments(int classId)
        {
            var foundClass = _classRepository.GetFirst(cl => cl.Id == classId,
                cl => cl.Include(c => c.ClassAssignments));

            if (foundClass is null)
                return null;
            return foundClass.ClassAssignments.OrderBy(a => a.Priority)
                .ThenByDescending(assignment => assignment.Id)
                .ToList();
        }

        public Assignment AddNewClassAssignment(int classId, string name, int point)
        {
            var foundClass = GetClassDetail(classId);
            var newAssignment = new Assignment {Name = name, Weight = point, Priority = 0, Class = foundClass};


            _assignmentRepository.Insert(newAssignment);

            return newAssignment;
        }

        public bool RemoveAssignment(int assignmentId)
        {
            var foundAssignment = _assignmentRepository.GetFirst(assignment => assignment.Id == assignmentId);
            if (foundAssignment is null)
                return false;
            _assignmentRepository.Delete(foundAssignment);
            return true;
        }

        public Assignment UpdateClassAssignment(int assignmentId, string newName, int newPoint)
        {
            var foundAssignment = _assignmentRepository.GetFirst(assignment => assignment.Id == assignmentId,
                assignment
                    => assignment.Include(a => a.Class));
            if (foundAssignment is null)
                return null;
            foundAssignment.Name = newName;
            foundAssignment.Weight = newPoint;
            _assignmentRepository.Update(foundAssignment);

            return foundAssignment;
        }

        public List<Assignment> UpdateClassAssignmentPriority(int classId, List<int> newOrder)
        {
            var foundClass = GetClassDetail(classId);
            foundClass.UpdateAssignmentsPriority(newOrder);


            _classRepository.Update(foundClass);
            return foundClass.ClassAssignments.OrderBy(assignment => assignment.Priority)
                .ThenByDescending(assignment => assignment.Id)
                .ToList();
        }

  


        public List<StudentAssignmentGrade> BulkAddStudentGradeToAssignment(int assignmentId,
            List<Tuple<string, int>> idGradePairs)
        {
            var foundAssignment = _assignmentRepository.GetFirst(a => a.Id == assignmentId,
                a =>
                    a.Include(ass => ass.StudentAssignmentGrades)
                        .Include(ass => ass.Class)
                        .ThenInclude(c => c.Students));
            if (foundAssignment is null)
                return null;
            foundAssignment.AddStudentGrades(idGradePairs);
            foundAssignment.SetAllFinalizedStatus(false);
            _assignmentRepository.Update(foundAssignment);
            return foundAssignment.StudentAssignmentGrades.ToList();
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

        public List<Assignment> GetAllClassAssignmentWithGradeAsTeacher(int classId)
        {
            var foundClass = _classRepository.GetFirst(cl => cl.Id == classId,
                cl => cl.Include(c => c.ClassAssignments)
                    .ThenInclude(a => a.StudentAssignmentGrades));

            if (foundClass is null)
                return null;
            return foundClass.ClassAssignments.OrderBy(a => a.Priority)
                .ThenByDescending(a => a.Id)
                .ToList();
        }

        public List<Assignment> GetAllClassAssignmentWithGradeAsStudent(int classId, int userId)
        {
            var foundUser = _userRepository.GetFirst(u => u.Id == userId);
            if (foundUser is null)
                return null;
            var assignments = _assignmentRepository.List(a => a.Class.Id == classId,
                    null, a => a.Include(ass => ass.Class)
                        .Include(ass => ass.StudentAssignmentGrades).ThenInclude(sg => sg.StudentRecord))
                .OrderBy(a => a.Priority).ThenByDescending(a => a.Id)
                .ToList();

            foreach (var assignment in assignments)
            {
                var sGrades = assignment.StudentAssignmentGrades.ToList();
                sGrades = sGrades.Where(sg =>
                        sg.StudentRecord.StudentIdentification == foundUser.StudentIdentification)
                    .ToList();
                assignment.SetStudentAssignmentGrades(sGrades);
            }

            return assignments;
        }

        public Assignment UpdateAssignmentFinalization(int assignmentId, bool newStatus)
        {
            var foundAssignment = _assignmentRepository.GetFirst(a => a.Id == assignmentId,
                a => a.Include(ass => ass.StudentAssignmentGrades));

            foundAssignment.SetAllFinalizedStatus(newStatus);
            _assignmentRepository.Update(foundAssignment);
            return foundAssignment;
        }

        public List<Assignment> UpdateWholeClassAssignmentFinalization(int classId, bool newStatus)
        {
            var foundClass = _classRepository.GetFirst(c => c.Id == classId,
                c => c.Include(cl => cl.ClassAssignments)
                    .ThenInclude(a => a.StudentAssignmentGrades));
            foundClass.SetAllAssignmentFinalizeStatus(newStatus);
            _classRepository.Update(foundClass);
            return foundClass.ClassAssignments.OrderBy(a => a.Priority).ThenByDescending(a => a.Id).ToList();
        }

        public StudentAssignmentGrade UpdateStudentAssignmentGrade(int assignmentId, string studentIdentification,
            bool newStatus,
            int newPoint)
        {
            var foundAssignment = _assignmentRepository.GetFirst(a => a.Id == assignmentId,
                ass => ass.Include(a => a.Class));

            var foundSGrade = _sGradeRepository.GetFirst(sg => sg.AssignmentId == assignmentId
                                                               && sg.StudentRecord.StudentIdentification ==
                                                               studentIdentification &&
                                                               sg.Assignment.Class.Id == foundAssignment.Class.Id,
                sGrade =>
                    sGrade.Include(sg => sg.StudentRecord)
                        .Include(sg => sg.Assignment)
                        .ThenInclude(a => a.Class)
            );
            if (foundSGrade is null)
            {
                var foundStudentRecord =
                    _studentRecordRepository.GetFirst(sr =>
                        sr.StudentIdentification == studentIdentification && sr.ClassId == foundAssignment.Class.Id);
                // create new student grade
                var newSGrade = new StudentAssignmentGrade();
                newSGrade.AssignmentId = assignmentId;
                newSGrade.IsFinalized = newStatus;
                newSGrade.Point = newPoint;
                newSGrade.StudentRecordId = foundStudentRecord.Id;
                _sGradeRepository.Insert(newSGrade);
                return newSGrade;
            }

            foundSGrade.Point = newPoint;
            foundSGrade.IsFinalized = newStatus;

            _sGradeRepository.Update(foundSGrade);

            return foundSGrade;
        }

        private Class GetClassDetail(int classId)
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
    }
}