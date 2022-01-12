namespace grade_book_api.Requests
{
    public class TryConfirmCodeWhenForgotPassword
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
    }
}