using System;

namespace grade_book_api.Responses.Class
{
    public class ClassShortInformationResponse
    {
        
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public string InviteStringTeacher { get; set; }
        public string InviteStringStudent { get; set; }

        public ClassShortInformationResponse(ApplicationCore.Entity.Class inputClass)
        {
            Name = inputClass.Name;
            StartDate = inputClass.StartDate;
            Room = inputClass.Room;
            Description = inputClass.Description;
            InviteStringStudent = inputClass.InviteStringStudent;
            InviteStringTeacher = inputClass.InviteStringTeacher;
        }
    }
}