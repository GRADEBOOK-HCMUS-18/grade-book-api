using System;
using System.Collections.Generic;
using System.Linq;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class ClassDetailInformationResponse
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }

        public UserInformationResponse MainTeacher { get; set; }
        public List<UserInformationResponse> SubTeachers { get; set; } = new List<UserInformationResponse>();
        public List<UserInformationResponse> Students { get; set; } = new List<UserInformationResponse>();

        public ClassDetailInformationResponse(ApplicationCore.Entity.Class inputClass, bool includeInviteStrings)
        {
            Id = inputClass.Id;
            Name = inputClass.Name;
            StartDate = inputClass.StartDate;
            Room = inputClass.Room;
            Description = inputClass.Description;
            InviteStringStudent = includeInviteStrings ?  inputClass.InviteStringStudent : "";
            InviteStringTeacher = includeInviteStrings ?  inputClass.InviteStringTeacher: "";
            MainTeacher = new UserInformationResponse(inputClass.MainTeacher);
            SubTeachers = inputClass.ClassTeachers.Select(ct => new UserInformationResponse(ct.Teacher)).ToList();
            Students = inputClass.ClassStudents.Select(cs => new UserInformationResponse(cs.Student)).ToList();

        }
        
        
    }
}