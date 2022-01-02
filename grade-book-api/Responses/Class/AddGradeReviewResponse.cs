using ApplicationCore.Entity;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class AddGradeReviewResponse
    {
        
        public int RequestedNewPoint { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public AssignmentGradeResponse CurrentGrade { get; set; }
        public UserInformationResponse User { get; set; }


        public AddGradeReviewResponse(AssignmentGradeReviewRequest sourceRequest
            , ApplicationCore.Entity.User sourceUser )
        {
            RequestedNewPoint = sourceRequest.RequestedNewPoint;
            Description = sourceRequest.Description;
            State = sourceRequest.RequestState switch
            {
                ReviewRequestState.Accepted => "Accepted",
                ReviewRequestState.Waiting => "Waiting",
                ReviewRequestState.Rejected => "Rejected",
                _ => "Waiting"
            };
            CurrentGrade = new AssignmentGradeResponse(sourceRequest.StudentAssignmentGrade);
            User = new UserInformationResponse(sourceUser);
        }
    }
}