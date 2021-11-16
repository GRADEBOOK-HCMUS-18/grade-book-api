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
            ProfilePictureUrl = user.ProfilePictureUrl;
            DefaultProfilePictureHex = user.DefaultProfilePictureHex;
        }

        public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

        public string DefaultProfilePictureHex { get; set; }
    }
}