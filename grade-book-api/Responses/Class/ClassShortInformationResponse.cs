using System;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class ClassShortInformationResponse
    {
        public ClassShortInformationResponse(ApplicationCore.Entity.Class inputClass, string roleOfCurrentUser,
            ApplicationCore.Entity.User mainTeacher)
        {
            Name = inputClass.Name;
            StartDate = inputClass.StartDate;
            Room = inputClass.Room;
            Description = inputClass.Description;
            Id = inputClass.Id;
            RoleOfCurrentUser = roleOfCurrentUser;
            InviteStringTeacher = roleOfCurrentUser != "student" ? inputClass.InviteStringTeacher : "";
            InviteStringStudent = roleOfCurrentUser != "student" ? inputClass.InviteStringStudent : "";
            MainTeacher = new UserInformationResponse(mainTeacher);
        }

        public int Id { get; set; }
        public string RoleOfCurrentUser { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }

        public UserInformationResponse MainTeacher { get; set; }
    }
}