using System;

namespace grade_book_api.Responses.Class
{
    public class ClassShortInformationResponse
    {
        public ClassShortInformationResponse(ApplicationCore.Entity.Class inputClass, string roleOfCurrentUser)
        {
            Name = inputClass.Name;
            StartDate = inputClass.StartDate;
            Room = inputClass.Room;
            Description = inputClass.Description;
            InviteStringStudent = inputClass.InviteStringStudent;
            InviteStringTeacher = inputClass.InviteStringTeacher;
            Id = inputClass.Id;
            RoleOfCurrentUser = roleOfCurrentUser;
        }

        public int Id { get; set; }
        public string RoleOfCurrentUser { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }
    }
}