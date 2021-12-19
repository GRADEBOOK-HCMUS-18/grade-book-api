using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class ShortStudentGradeResponse
    {
        public int AssigmentId { get; set; }
        public string AssignmentName { get; set; }
        public int AssignmentWeight { get; set; }
        public int? StudentPoint { get; set; }
    }
    public class GradeBoardDetailResponse
    {
        public StudentRecordResponse Student { get; set; }

        public List<ShortStudentGradeResponse> Grades { get; set; } = new List<ShortStudentGradeResponse>();

        public GradeBoardDetailResponse(StudentRecord studentRecord, List<Assignment> assignments)
        {
            Student = new StudentRecordResponse(studentRecord);
            foreach (var assignment in assignments)
            {
                var toAdd = new ShortStudentGradeResponse();
                var sGrades = assignment.StudentAssignmentGrades.ToList();
                var result = sGrades
                    .FirstOrDefault(sg =>
                        sg.StudentRecordId == studentRecord.RecordId && sg.AssignmentId == assignment.Id);
                toAdd.AssigmentId = assignment.Id;
                toAdd.AssignmentName = assignment.Name;
                toAdd.AssignmentWeight = assignment.Weight;
                toAdd.StudentPoint = result?.Point;
                
                Grades.Add(toAdd);
            }
        }
    }
}