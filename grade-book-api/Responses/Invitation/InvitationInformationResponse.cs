using grade_book_api.Responses.Class;

namespace grade_book_api.Responses.Invitation
{
    public class InvitationInformationResponse
    {
        
        public bool IsTeacherInvitation { get; set; }
        public  ClassShortInformationResponse ClassInformation { get; set; }


        public InvitationInformationResponse(ApplicationCore.Entity.Class inputClass, bool isTeacherInvitation)
        {
            ClassInformation = new ClassShortInformationResponse(inputClass, "none", inputClass.MainTeacher);
            IsTeacherInvitation = isTeacherInvitation;
        }
    }
}