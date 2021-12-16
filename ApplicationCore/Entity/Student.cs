using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entity
{
    public class Student
    {
        public int RecordId; 
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public string StudentIdentification { get;set; }
        public string FullName { get; set; }
        
        public IList<StudentAssignmentGrade> Grades { get; set; }
    }
}