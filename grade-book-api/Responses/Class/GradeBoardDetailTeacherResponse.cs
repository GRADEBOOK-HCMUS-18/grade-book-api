using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace grade_book_api.Responses.Class
{
    public class ShortStudentGradeResponse
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentWeight { get; set; }
        public int? StudentPoint { get; set; }

        public bool IsFinal { get; set; }
    }

    public class GradeBoardDetailTeacherResponse
    {
        public StudentRecordResponse Student { get; set; }

        public List<ShortStudentGradeResponse> Grades { get; set; } = new();

        public GradeBoardDetailTeacherResponse(StudentRecord studentRecord, List<Assignment> assignments)
        {
            Student = studentRecord is not null ? new StudentRecordResponse(studentRecord) : null;
            foreach (var assignment in assignments)
            {
                var toAdd = new ShortStudentGradeResponse();
                var sGrades = assignment.StudentAssignmentGrades.ToList();

                toAdd.AssignmentId = assignment.Id;
                toAdd.AssignmentName = assignment.Name;
                toAdd.AssignmentWeight = assignment.Weight;
                if (studentRecord is not null)
                {
                    var result = sGrades
                        .FirstOrDefault(sg =>
                            sg.StudentRecordId == studentRecord.Id && sg.AssignmentId == assignment.Id);
                    toAdd.StudentPoint = result?.Point;
                    toAdd.IsFinal = result is null ? false : result.IsFinalized;

                    Grades.Add(toAdd);
                }
            }
        }
    }
}