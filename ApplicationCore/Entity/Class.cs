using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entity
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }
        public User MainTeacher { get; set; }
        public IList<ClassStudentsAccount> ClassStudentsAccounts { get; set; } = new List<ClassStudentsAccount>();
        public IList<ClassTeachersAccount> ClassTeachersAccounts { get; set; } = new List<ClassTeachersAccount>();

        public IList<StudentRecord> Students { get; set; } = new List<StudentRecord>(); 

        public IList<Assignment> ClassAssignments { get; set; } = new List<Assignment>();


        public StudentRecord FindStudent(string studentId)
        {
            return Students.FirstOrDefault(s => s.StudentIdentification == studentId); 
        }

        public void AddStudents(List<Tuple<string, string>> idNamePairs)
        {
            
            var toAdd = new List<StudentRecord>();
            foreach (var (item1, item2) in idNamePairs)
            {
                var foundStudent = Students.FirstOrDefault(student => student.StudentIdentification == item1);
                if (foundStudent is not null)
                {
                    foundStudent.FullName = item2;
                }
                else
                {
                    toAdd.Add(new StudentRecord
                    {
                        ClassId = this.Id,
                        StudentIdentification = item1,
                        FullName = item2
                    });
                }
            }

            foreach (var student in toAdd)
            {
                this.Students.Add(student);
            }
        }
    }
}