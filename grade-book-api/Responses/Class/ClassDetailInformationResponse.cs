using System;
using System.Collections.Generic;
using System.Linq;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class ClassDetailInformationResponse
    {
        public ClassDetailInformationResponse(ApplicationCore.Entity.Class inputClass, bool isTeacher)
        {
            Id = inputClass.Id;
            Name = inputClass.Name;
            StartDate = inputClass.StartDate;
            Room = inputClass.Room;
            Description = inputClass.Description;
            InviteStringStudent = isTeacher ? inputClass.InviteStringStudent : "";
            InviteStringTeacher = isTeacher ? inputClass.InviteStringTeacher : "";
            IsTeacher = isTeacher;
            MainTeacher = new UserInformationResponse(inputClass.MainTeacher);
            SubTeachers = inputClass.ClassTeachers.Select(ct => new UserInformationResponse(ct.Teacher)).ToList();
            Students = inputClass.ClassStudents.Select(cs => new UserInformationResponse(cs.Student)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }
        
        public bool IsTeacher { get; set; }

        public UserInformationResponse MainTeacher { get; set; }
        public List<UserInformationResponse> SubTeachers { get; set; } = new();
        public List<UserInformationResponse> Students { get; set; } = new();
    }
}