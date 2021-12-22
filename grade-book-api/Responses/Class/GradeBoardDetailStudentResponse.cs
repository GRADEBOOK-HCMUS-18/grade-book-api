using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class GradeBoardDetailStudentResponse
    {
       public StudentRecordResponse Student { get; set; }
       
               public List<ShortStudentGradeResponse> Grades { get; set; } = new List<ShortStudentGradeResponse>();
       
               public GradeBoardDetailStudentResponse(StudentRecord studentRecord, List<Assignment> assignments)
               {
                   Student = (studentRecord is not null) ? new StudentRecordResponse(studentRecord) : null;
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
                           toAdd.IsFinal = (result is null) ? false : result.IsFinalized;
                           if (!toAdd.IsFinal)
                           {
                               toAdd.StudentPoint = null; 
                           }
                       
                           Grades.Add(toAdd);
                           
                       }
                   }
               }
    }
}