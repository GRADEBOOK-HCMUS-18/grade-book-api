using System.Collections.Generic;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IReviewService
    {
        AssignmentGradeReviewRequest AddReviewRequest(int assignmentId
            , int userId
            , int requestedPoint
            , string description);

        List<AssignmentGradeReviewRequest> GetReviewRequestsAsTeacher( int classId);

        List<AssignmentGradeReviewRequest> GetReviewRequestsAsStudent(int classId, string studentIdentification);

        GradeReviewReply AddReviewReplyAsTeacher(int userId, int reviewId, string content);
        GradeReviewReply AddReviewReplyAsStudent(int userId, int reviewId, string content);

        void UpdateGradeReviewState(int reviewId, string newState); 
    }
}