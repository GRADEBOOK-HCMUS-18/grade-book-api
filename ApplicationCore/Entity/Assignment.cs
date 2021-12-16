using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; }
        public int Point { get; set; }
        public int Priority { get; set; }
        public Class Class { get; set; }
        
        public IList<StudentAssignmentGrade> StudentAssignmentGrades { get; set; }

        public void AddStudentGrade(List<Tuple<string, int>> idGradePairs)
        {
            // foreach (var (studentId, grade) in idGradePairs)
            // {
            //    var existedRecord = StudentAssignmentGrades.FirstOrDefault(sGrade => sGrade)
            // }
        }
    }
}