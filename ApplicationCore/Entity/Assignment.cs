using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Priority { get; set; }
        public Class Class { get; set; }

        public IList<StudentAssignmentGrade> StudentAssignmentGrades { get; set; } = new List<StudentAssignmentGrade>();

        public void SetStudentAssignmentGrades(List<StudentAssignmentGrade> source)
        {
            StudentAssignmentGrades = source;
        }

        public void AddStudentGrades(List<Tuple<string, int>> idGradePairs)
        {
            var toAdd = new List<StudentAssignmentGrade>();
            foreach (var (id, grade) in idGradePairs)
            {
                var foundStudentRecord = Class.FindStudent(id);
                if (foundStudentRecord is null)
                    throw new ConstraintException(
                        $"Student with Id {id} is not in this class, consider adding her/him");
                toAdd.Add(new StudentAssignmentGrade
                {
                    StudentRecord = foundStudentRecord,
                    StudentRecordId = foundStudentRecord.Id,
                    Point = grade,
                    IsFinalized = false,
                    AssignmentId = Id
                });
            }

            StudentAssignmentGrades.Clear();
            foreach (var sGrade in toAdd) StudentAssignmentGrades.Add(sGrade);
        }

        public void SetAllFinalizedStatus(bool newStatus)
        {
            foreach (var sGrade in StudentAssignmentGrades) sGrade.IsFinalized = newStatus;
        }
    }
}