namespace grade_book_api.Requests.ClassRequests
{
    public class AddStudentToClassRequest
    {
       public int StudentId { get; set; }
       public int ClassId { get; set; }
    }
}