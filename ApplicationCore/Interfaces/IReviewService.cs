using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IReviewService
    {
        AssignmentGradeReviewRequest AddReviewRequest(int assignmentId
            , int userId
            , int requestedPoint
            , string description); 
    }
}