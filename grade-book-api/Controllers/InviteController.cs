using System;
using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Requests;
using grade_book_api.Responses.Invitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [Route("[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IEmailSender _emailSender;
        private readonly IInvitationService _invitationService;
        private readonly ILogger<InviteController> _logger;
        private readonly IUserServices _userService;

        public InviteController(ILogger<InviteController> logger, IInvitationService invitationService,
            IUserServices userServices,
            IClassService classService,
            IEmailSender emailSender)
        {
            _logger = logger;
            _invitationService = invitationService;
            _userService = userServices;
            _classService = classService;
            _emailSender = emailSender;
        }

        [Authorize]
        [HttpPost("email/send")]
        public IActionResult TryEmail([FromBody] EmailSendingRequest request)
        {
            var htmlMessage = $"{request.MailContent} : <a href=\"{request.UrlToSend}\">Link</a>";
            try
            {
                _emailSender
                    .BulkSendEmail(request.MailList, $"GradeBook: {request.MailSubject}", htmlMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{inviteString}")]
        public IActionResult GetInviteInformation([FromRoute] string inviteString)
        {
            var userId = GetCurrentUserIdFromToken();
            _logger.LogInformation($"Received invitation information request for {inviteString}");
            var foundClass = _invitationService.GetClassFromInvitation(inviteString);
            if (foundClass is null) return NotFound("Invitation string does not exist");

            var userRoleInClass = _userService.GetUserRoleInClass(userId, foundClass.Id);

            var isTeacherInvite = inviteString == foundClass.InviteStringTeacher;
            var response = new InvitationInformationResponse(foundClass, userRoleInClass, isTeacherInvite);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("{inviteString}")]
        public IActionResult AcceptInvite([FromRoute] string inviteString)
        {
            var userId = GetCurrentUserIdFromToken();
            var foundClass = _invitationService.GetClassFromInvitation(inviteString);

            var isTeacherInvite = inviteString == foundClass.InviteStringTeacher;
            try
            {
                if (isTeacherInvite)
                    _classService.AddTeacherAccountToClass(foundClass.Id, userId);
                else
                    _classService.AddStudentAccountToClass(foundClass.Id, userId);
                return Ok(
                    $"User with id {userId} added to class {foundClass.Name} with role: {(isTeacherInvite ? "teacher" : "student")} ");
            }
            catch (ApplicationException exception)
            {
                return BadRequest(exception.Message);
            }
        }


        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }
    }
}