using ApplicationCore.Entity;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class StudentGradeWithAssignmentAndStudentSpec: Specification<StudentAssignmentGrade>
    {
        public StudentGradeWithAssignmentAndStudentSpec(int assigmentId, string studentIdentification)
        {
            Query
                .Where(sGrade => sGrade.Assignment.Id == assigmentId && sGrade.StudentRecord.StudentIdentification == studentIdentification)
                .Include(sGrade => sGrade.AssignmentGradeReviewRequests)
                .ThenInclude(r => r.GradeReviewReplies)
                .ThenInclude(reply => reply.Replier)
                .Include(sGrade => sGrade.StudentRecord)
                .Include(sGrade => sGrade.Assignment);
        }
    }
}