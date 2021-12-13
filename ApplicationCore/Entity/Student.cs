using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entity
{
    public class Student
    {
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public string StudentIdentification { get;set; }
        public string FullName { get; set; }
    }
}