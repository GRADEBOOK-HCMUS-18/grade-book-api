namespace grade_book_api.Responses.User
{
    public class UserInformationResponse
    {
        public UserInformationResponse(ApplicationCore.Entity.User source)
        {
            Email = source.Email;
            FirstName = source.FirstName;
            LastName = source.LastName;
            ProfilePictureUrl = source.ProfilePictureUrl;
            DefaultProfilePictureHex = source.DefaultProfilePictureHex;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string DefaultProfilePictureHex { get; set; }
    }
}