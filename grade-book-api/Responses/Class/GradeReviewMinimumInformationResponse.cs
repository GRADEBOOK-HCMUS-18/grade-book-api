using ApplicationCore.Entity;

namespace grade_book_api.Responses.Class
{
    public class GradeReviewMinimumInformationResponse
    {
        
        public int Id { get; set; }
        public int RequestedNewPoint { get; set; }
        public string Description { get; set; }
        public string State { get; set; }

        public GradeReviewMinimumInformationResponse(AssignmentGradeReviewRequest source)
        {
            Id = source.Id;
            RequestedNewPoint = source.RequestedNewPoint;
            Description = source.Description;
            
            State = source.RequestState switch
            {
                ReviewRequestState.Accepted => "Accepted",
                ReviewRequestState.Waiting => "Waiting",
                ReviewRequestState.Rejected => "Rejected",
                _ => "Waiting"
            };
        }
    }
}