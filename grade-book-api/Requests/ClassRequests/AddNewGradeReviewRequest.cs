using grade_book_api.Responses.User;

namespace grade_book_api.Requests.ClassRequests
{
    public class AddNewGradeReviewRequest
    {
        public int RequestedNewPoint {get;set;}
        public string Description { get; set; }
        
 
    }
}