using System.Collections.Generic;
using ApplicationCore.Entity;
using ApplicationCore.Services;

namespace ApplicationCore.Interfaces
{
    public interface IUserNotificationService
    {
        List<UserNotification> ReadPagedUserNotification(int userId,int pageNumber, int numOfNotificationPerPage);

        void SetUserNotificationAsViewed(int userId);

        void AddNewFinalizedGradeCompositionNotification(int assignmentId);

        UserNotification AddNewGradeRequestNotification(AssignmentGradeReviewRequest request);

        UserNotification AddNewGradeReviewReplyNotification(GradeReviewReply reply);

        UserNotification AddAcceptedOrRejectedGradeReviewNotification(AssignmentGradeReviewRequest request);

    }
}