namespace grade_book_api.Responses.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set;  }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl{ get; set; }
    }

}