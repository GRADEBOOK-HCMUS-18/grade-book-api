using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Requests.Admin;
using grade_book_api.Responses.Admin;
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
        public AdminApiController(IUserServices userServices, IAdminService adminService)
        {
            _userServices = userServices;
            _adminService = adminService;
        }

        [HttpGet("user")]

        public IActionResult GetPagedUserList([FromQuery] int numberPerPage, [FromQuery] int pageNumber)
        {
            if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken()))
            {
                return Unauthorized(UnauthorizedString);
            }
            var userList = _adminService.GetPagedUsersList(numberPerPage, pageNumber);
            var total = _adminService.CountTotalUser();

            var response = new PagedUserListResponse(numberPerPage, pageNumber, total, userList);

            return Ok(response);

        }

        [HttpGet("user/{userId}")]

        public IActionResult GetSingleUserInformation(int userId)
        {
             if (!_userServices.IsUserAdmin(GetCurrentUserIdFromToken()))
             {
                 return Unauthorized(UnauthorizedString);
             }

             var user = _userServices.GetUserById(userId);
             return Ok(new UserDetailedInformationResponse(user));
        }

        [HttpPut("user/{userId}/lockState")]
        public IActionResult ChangeLockedStateOfAUser(int userId)
        {
            return Ok("");
        }

        [HttpGet("class")]
        public IActionResult GetPagedClassList()
        {
            return Ok("doing");
        }
        
        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }

    }
}