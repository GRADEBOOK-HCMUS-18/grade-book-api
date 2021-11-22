namespace grade_book_api.Responses.Authentication
{
    public class LoginResponse
    {
        public LoginResponse(ApplicationCore.Entity.User user, string token)
        {
            Token = token;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            StudentIdentification = user.StudentIdentification;
            ProfilePictureUrl = user.ProfilePictureUrl;
            DefaultProfilePictureHex = user.DefaultProfilePictureHex;
            IsPasswordNotSet = user.IsPasswordNotSet;
        }


        public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentIdentification { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string DefaultProfilePictureHex { get; set; }
        public bool IsPasswordNotSet { get; set; }
    }
}