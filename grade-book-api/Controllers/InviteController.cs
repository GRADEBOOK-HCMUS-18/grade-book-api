using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using grade_book_api.Responses.Class;
using grade_book_api.Responses.Invitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [Route("[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly ILogger<InviteController> _logger;
        private readonly IInvitationService _invitationService;

        public InviteController(ILogger<InviteController> logger, IInvitationService invitationService)
        {
            _logger = logger;
            _invitationService = invitationService;
        }
        
        [HttpGet]
        [Route("{inviteString}")]
        public IActionResult GetInviteInformation([FromRoute] string inviteString)
        {
            _logger.LogInformation($"Received invitation information request for {inviteString}");
            Class foundClass = _invitationService.GetClassFromInvitation(inviteString);
            if (foundClass is null)
            {
                return NotFound("Invitation string does not exist");
            }

            bool isTeacherInvite = inviteString == foundClass.InviteStringTeacher;
            var response = new InvitationInformationResponse(foundClass, isTeacherInvite);
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