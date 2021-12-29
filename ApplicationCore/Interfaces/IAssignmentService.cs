using System;
using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IAssignmentService
    {
        List<Assignment> GetClassAssignments(int classId);
        Assignment AddNewClassAssignment(int classId, string name, int point);

        bool RemoveAssignment(int assignmentId);

        Assignment UpdateClassAssignment(int assignmentId, string newName, int newPoint);
        List<Assignment> UpdateClassAssignmentPriority(int classId, List<int> newOrder);

        List<StudentAssignmentGrade> BulkAddStudentGradeToAssignment(int assignmentId,
            List<Tuple<string, int>> idGradePairs);
        List<Assignment> GetAllClassAssignmentWithGradeAsTeacher(int classId);
        List<Assignment> GetAllClassAssignmentWithGradeAsStudent(int classId, int userId);

        // update assignments finalization and point
        Assignment UpdateAssignmentFinalization(int assignmentId, bool newStatus);
        List<Assignment> UpdateWholeClassAssignmentFinalization(int classId, bool newStatus);

        StudentAssignmentGrade UpdateStudentAssignmentGrade(int assignmentId, string studentIdentification,
            bool newStatus,
            int newPoint);
    }
}