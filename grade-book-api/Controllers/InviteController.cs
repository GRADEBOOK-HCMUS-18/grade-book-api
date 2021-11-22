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
        private readonly IInvitationService _invitationService;
        private readonly ILogger<InviteController> _logger;
        private readonly IUserServices _userService;
        private readonly IEmailSender _emailSender;

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

        [HttpPut]
        [Route("email")]
        public IActionResult TryEmail([FromBody] EmailSendingRequest request)
        {
            string htmlMessage = $"{request.MailContent}  <a>{request.UrlToSend}</a>";
            try
            {
                _emailSender.BulkSendEmail(request.MailList, "Test 5th", htmlMessage);
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
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
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
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            var foundClass = _invitationService.GetClassFromInvitation(inviteString);

            var isTeacherInvite = inviteString == foundClass.InviteStringTeacher;
            try
            {
                if (isTeacherInvite)
                    _classService.AddTeacherToClass(foundClass.Id, userId);
                else
                    _classService.AddStudentToClass(foundClass.Id, userId);
                return Ok(
                    $"User with id {userId} added to class {foundClass.Name} with role: {(isTeacherInvite ? "teacher" : "student")} ");
            }
            catch (ApplicationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}