namespace grade_book_api.Requests.ClassRequests
{
    public class UpdateSingleStudentGradeRequest
    {
        public string StudentId { get; set; }
        public int NewPoint { get; set; }
        public bool NewStatus { get; set; }
    }
}