namespace grade_book_api.Responses.User
{
    public class UserDetailedInformationResponse
    {
        public UserDetailedInformationResponse(ApplicationCore.Entity.User source)
        {
            Id = source.Id;
            Email = source.Email;
            FirstName = source.FirstName;
            LastName = source.LastName;
            ProfilePictureUrl = source.ProfilePictureUrl;
            DefaultProfilePictureHex = source.DefaultProfilePictureHex;
            IsPasswordNotSet = source.IsPasswordNotSet;
            StudentIdentification = source.StudentIdentification;
            IsLocked = source.IsLocked;
            IsEmailConfirmed = source.IsEmailConfirmed;
        }
        
        public int Id { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentIdentification { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string DefaultProfilePictureHex { get; set; }
        public bool IsPasswordNotSet { get; set; }
        
        public bool IsLocked { get; set; }
        
        public bool IsEmailConfirmed { get; set; }
    }
}