using System.Collections.Generic;
using ApplicationCore.Entity;
using ApplicationCore.Services;

namespace ApplicationCore.Interfaces
{
    public interface IUserNotificationService
    {
        List<UserNotification> ReadPagedUserNotification(int userId,int pageNumber, int numOfNotificationPerPage);

        void SetSingleUserNotificationAsViewed(int notificationId);

        void SetAllUserNotificationAsViewed(int userId);

        int CountNotViewedNotification(int userId);

        void AddNewFinalizedGradeCompositionNotification(int assignmentId);

        void AddNewGradeRequestNotification(int requestId);

        void AddNewGradeReviewReplyNotification(int replyId);

        void AddAcceptedOrRejectedGradeReviewNotification(int requestId);

    }
}