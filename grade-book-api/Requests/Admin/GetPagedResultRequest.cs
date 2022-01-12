namespace grade_book_api.Requests.Admin
{
    public class GetPagedResultRequest
    {
        public int NumberPerPage { get; set; }
        public int PageNumber {get; set; }
    }
}