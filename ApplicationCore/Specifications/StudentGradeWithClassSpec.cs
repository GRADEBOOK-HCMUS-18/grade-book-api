using ApplicationCore.Entity;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApplicationCore.Specifications
{
    public class StudentGradeWithClassSpec : Specification<StudentAssignmentGrade>
    {
        public StudentGradeWithClassSpec(int classId)
        {
            Query
                .Where(sGrade => sGrade.Assignment.Class.Id == classId)
                .Include(sGrade => sGrade.AssignmentGradeReviewRequests)
                .ThenInclude(r => r.GradeReviewReplies)
                .ThenInclude(reply => reply.Replier)
                .Include(sGrade => sGrade.StudentRecord)
                .Include(sGrade => sGrade.Assignment)
                .ThenInclude(sGrade => sGrade.Class);
        }
        // student is allow to see their grade only 
        public StudentGradeWithClassSpec(int classId, string currentStudentIdentification): this(classId )
        {
            Query.Where(sGrade => sGrade.StudentRecord.StudentIdentification == currentStudentIdentification); 
        }
    }
}