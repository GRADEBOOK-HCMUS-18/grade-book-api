using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using grade_book_api.Responses.User;

namespace grade_book_api.Responses.Class
{
    public class GradeReviewInformationResponse
    {
        public int Id { get; set; }
        public int RequestedNewPoint { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public AssignmentGradeResponse CurrentGrade { get; set; }
        public StudentRecordResponse Student { get; set; }
        
        public List<ReviewReplyResponse> Replies { get; set; }


        public GradeReviewInformationResponse(AssignmentGradeReviewRequest sourceRequest
           )
        {
            Id = sourceRequest.Id;
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
            Student = new StudentRecordResponse(sourceRequest.StudentAssignmentGrade.StudentRecord);
            Replies = sourceRequest.GradeReviewReplies.Select(reply => new ReviewReplyResponse(reply)).ToList();
        }
    }
}