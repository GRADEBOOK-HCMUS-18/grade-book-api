using System;
using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IClassService
    {
        Class GetClassDetail(int classId);
        List<Class> GetAllClassWithUserBeingMainTeacher(int userId);
        List<Class> GetAllClassWithUserBeingSubTeacher(int userId);
        List<Class> GetAllClassWithUserBeingStudent(int userId);
        Class AddNewClass(string name, DateTime startDate, string room, string description, int mainTeacherId);
        void AddStudentAccountToClass(int classId, int studentId);
        void AddTeacherAccountToClass(int classId, int teacherId);

        // assignments related

        List<Assignment> GetClassAssignments(int classId);
        Assignment AddNewClassAssignment(int classId, string name, int point);

        bool RemoveAssignment(int assignmentId);

        Assignment UpdateClassAssignment(int assignmentId, string newName, int newPoint);
        List<Assignment> UpdateClassAssignmentPriority(int classId, List<int> newOrder);

        List<StudentAssignmentGrade> GetStudentAssignmentGradeAsTeacher(int assignmentId);
        
        

        List<StudentRecord> BulkAddStudentToClass(int classId, List<Tuple<string,string>> idNamePairs);

        List<StudentAssignmentGrade> BulkAddStudentGradeToAssignment(int assignmentId,
            List<Tuple<string, int>> idGradePairs);
        
        List<StudentRecord> GetStudentListInClass(int classId);

        StudentRecord GetStudentRecordOfUserInClass(int userId, int classId); 

        List<Assignment> GetAllClassAssignmentWithGradeAsTeacher(int classId);
        List<Assignment> GetAllClassAssignmentWithGradeAsStudent(int classId, int userId);
        
        // update assignments finalization and point
        Assignment UpdateAssignmentFinalization(int assignmentId, bool newStatus);
        List<Assignment> UpdateWholeClassAssignmentFinalization(int classId, bool newStatus);

        StudentAssignmentGrade UpdateStudentAssignmentGrade(int assignmentId, string studentIdentification, bool newStatus,
            int newPoint); 
    }
}