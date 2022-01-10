using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Responses.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grade_book_api.Controllers
{
    [Authorize]
    [Route("notification")]
    public class NotificationController: ControllerBase
    {
        private readonly IUserNotificationService _notificationService;

        public NotificationController(IUserNotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        
        [HttpGet]
        public IActionResult GetPagedNotification([FromQuery] int notificationPerPage, [FromQuery] int pageNumber)
        {
            int userId = GetCurrentUserIdFromToken();
            var listNotifications = _notificationService
                .ReadPagedUserNotification(userId, pageNumber, notificationPerPage);

            int numberOfNotViewedNotification = _notificationService.CountNotViewedNotification(userId);
            var response = new NotificationListResponse(pageNumber,listNotifications,numberOfNotViewedNotification);
            
            _notificationService.SetUserNotificationAsViewed(userId);

            return Ok(response);
        }
        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }
    }
}