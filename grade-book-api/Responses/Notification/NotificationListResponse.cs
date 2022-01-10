using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;

namespace grade_book_api.Responses.Notification
{
    public class NotificationListResponse
    {
        public int NumberOfNotViewedNotification { get; set; }
        public int PageNumber { get; set; }
        public List<NotificationResponse> Notifications { get; set; }

        public NotificationListResponse(int pageNumber, List<UserNotification> userNotifications, int numberOfNotViewedNotification)
        {
            PageNumber = pageNumber;
            Notifications = userNotifications.Select(n => new NotificationResponse(n)).ToList();
            NumberOfNotViewedNotification = numberOfNotViewedNotification;
        }
    }
}