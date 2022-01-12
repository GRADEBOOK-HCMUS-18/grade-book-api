using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Requests.Admin;
using grade_book_api.Responses.Admin;
using grade_book_api.Responses.Class;
using grade_book_api.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AdminApiController: ControllerBase
    {
        public string UnauthorizedString = "Only admin is allowed";
        public string UnauthorizedAdminString = "Only super admin is allowed";
        private readonly IUserServices _userServices;
        private readonly IAdminService _adminService;
        private readonly IClassService _classService;
        public AdminApiController(IUserServices userServices, IAdminService adminService, IClassService classService)
        {
            _userServices = userServices;
            _adminService = adminService;
            _classService = classService;
        }

        [HttpGet("user")]

        public IActionResult GetPagedUserList([FromQuery] int numberPerPage, [FromQuery] int pageNumber)
        {
            if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken())) return Unauthorized(UnauthorizedString);
            var userList = _adminService.GetPagedUsersList(numberPerPage, pageNumber);
            var total = _adminService.CountTotalUser();

            var response = new PagedUserListResponse(numberPerPage, pageNumber, total, userList);

            return Ok(response);

        }

        [HttpGet("user/{userId}")]

        public IActionResult GetSingleUserInformation(int userId)
        {
            if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken())) return Unauthorized(UnauthorizedString);
            var user = _userServices.GetUserById(userId);
            if (user is null)
                return NotFound();
            return Ok(new UserDetailedInformationResponse(user));

        }

        [HttpPut("user/{userId}/lockState")]
        public IActionResult ChangeLockedStateOfAUser(int userId)
        {
            return Ok("");
        }

        [HttpGet("class")]
        public IActionResult GetPagedClassList([FromQuery] int numberPerPage, [FromQuery] int pageNumber)
        {
            if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken())) return Unauthorized(UnauthorizedString);
            var totalClass = _adminService.CountTotalClass();
            var classList = _adminService.GetPagedClassesList(numberPerPage, pageNumber);

            var response = new PagedClassListResponse(numberPerPage, pageNumber, totalClass, classList);

            return Ok(response);
        }

        [HttpGet("class/{classId}")]
        public IActionResult GetSingleClassDetail(int classId)
        {
            
            if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken())) return Unauthorized(UnauthorizedString);
            var foundClass = _classService.GetClassDetail(classId);

            if (foundClass is null)
                return NotFound();
            var response = new ClassDetailInformationResponse(foundClass, true);

            return Ok(response);

        }
        
        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }

    }
}