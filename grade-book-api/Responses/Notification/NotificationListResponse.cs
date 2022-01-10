using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;

namespace grade_book_api.Responses.Notification
{
    public class NotificationListResponse
    {
        public int PageNumber { get; set; }
        public List<NotificationResponse> Notifications { get; set; }

        public NotificationListResponse(int pageNumber, List<UserNotification> userNotifications)
        {
            PageNumber = pageNumber;
            Notifications = userNotifications.Select(n => new NotificationResponse(n)).ToList();
        }
    }
}