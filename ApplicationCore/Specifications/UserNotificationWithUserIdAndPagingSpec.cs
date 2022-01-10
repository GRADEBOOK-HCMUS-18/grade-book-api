using ApplicationCore.Entity;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class UserNotificationWithUserIdAndPagingSpec: Specification<UserNotification>
    {
        public UserNotificationWithUserIdAndPagingSpec(int userId, int notificationPerPage, int pageNumber)
        {
            Query.Include(n => n.Assignment)
                .Include(n => n.Class)
                .Include(n => n.AssignmentGradeReviewRequest)
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateTime)
                .Skip(notificationPerPage * (pageNumber - 1))
                .Take(notificationPerPage); 
                
        }
    }
}