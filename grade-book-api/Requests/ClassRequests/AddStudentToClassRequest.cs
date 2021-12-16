namespace grade_book_api.Requests.ClassRequests
{
    public class AddStudentToClassRequest
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
    }
}