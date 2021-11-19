using grade_book_api.Responses.Class;

namespace grade_book_api.Responses.Invitation
{
    public class InvitationInformationResponse
    {

        public bool IsAlreadyInClass { get; set; }

        public string CurrentRoleInClass { get; set; }
        public bool IsTeacherInvitation { get; set; }
        public ClassShortInformationResponse ClassInformation { get; set; }



        public InvitationInformationResponse(ApplicationCore.Entity.Class inputClass, int userRoleInClass,
            bool isTeacherInvitation)
        {
            CurrentRoleInClass = userRoleInClass switch
            {
                1 => "teacher",
                0 => "none",
                -1 => "student",
                _ => "none"

            };
            ClassInformation =
                new ClassShortInformationResponse(inputClass, CurrentRoleInClass, inputClass.MainTeacher);
            IsTeacherInvitation = isTeacherInvitation;
            IsAlreadyInClass = userRoleInClass != 0;

        }
    }
}