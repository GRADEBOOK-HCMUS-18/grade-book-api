using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entity
{
    public class StudentRecord : BaseEntity
    {
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public string StudentIdentification { get; set; }
        public string FullName { get; set; }

        public IList<StudentAssignmentGrade> Grades { get; set; } = new List<StudentAssignmentGrade>();
    }
}