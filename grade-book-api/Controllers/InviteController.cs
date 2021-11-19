using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Responses.Invitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [Route("[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        private readonly ILogger<InviteController> _logger;
        private readonly IUserServices _userService;

        public InviteController(ILogger<InviteController> logger, IInvitationService invitationService,
            IUserServices userServices)
        {
            _logger = logger;
            _invitationService = invitationService;
            _userService = userServices;
        }

        [HttpGet]
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
        public IActionResult AcceptInvite()
        {
            return Ok();
        }
    }
}