namespace grade_book_api.Requests.ClassRequests
{
    public class AddTeacherToClassRequest
    {
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
    }
}