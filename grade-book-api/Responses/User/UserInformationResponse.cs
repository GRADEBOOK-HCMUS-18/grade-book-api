namespace grade_book_api.Responses.User
{
    public class UserInformationResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string DefaultProfilePictureHex { get; set; }


        public UserInformationResponse(ApplicationCore.Entity.User source)
        {
            Username = source.Username;
            Email = source.Email;
            FirstName = source.FirstName;
            LastName = source.LastName;
            ProfilePictureUrl = source.ProfilePictureUrl;
            DefaultProfilePictureHex = source.DefaultProfilePictureHex;
        }
    }
}