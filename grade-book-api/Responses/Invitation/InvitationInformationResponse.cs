using ApplicationCore.Entity;
using grade_book_api.Responses.Class;

namespace grade_book_api.Responses.Invitation
{
    public class InvitationInformationResponse
    {
        public InvitationInformationResponse(ApplicationCore.Entity.Class inputClass, ClassRole userRoleInClass,
            bool isTeacherInvitation)
        {
            CurrentRoleInClass = userRoleInClass switch
            {
                ClassRole.Teacher => "teacher",
                ClassRole.NotAMember => "none",
                ClassRole.Student => "student",
                _ => "none"
            };
            ClassInformation =
                new ClassShortInformationResponse(inputClass, CurrentRoleInClass, inputClass.MainTeacher);
            IsTeacherInvitation = isTeacherInvitation;
            IsAlreadyInClass = userRoleInClass != ClassRole.NotAMember; 
        }

        public bool IsAlreadyInClass { get; set; }

        public string CurrentRoleInClass { get; set; }
        public bool IsTeacherInvitation { get; set; }
        public ClassShortInformationResponse ClassInformation { get; set; }
    }
}