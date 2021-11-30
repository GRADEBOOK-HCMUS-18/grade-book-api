using System;
using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Requests.ClassRequests;
using grade_book_api.Responses.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ILogger<ClassController> _logger;
        private readonly IUserServices _userServices;

        public ClassController(ILogger<ClassController> logger, IClassService classService, IUserServices userServices)
        {
            _logger = logger;
            _classService = classService;
            _userServices = userServices;
        }

        [HttpGet]
        [Route("{classId}")]
        public IActionResult GetClassDetail(int classId)
        {
            var userId = GetCurrentUserId();
            try
            {
                var foundClass = _classService.GetClassDetail(classId);
                var userRoleInClass = _userServices.GetUserRoleInClass(userId, classId);
                if (userRoleInClass == 0) return BadRequest("User not a member in class");
                var response = new ClassDetailInformationResponse(foundClass, userRoleInClass == 1);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetClassList()
        {
            var userId = GetCurrentUserId();
            try
            {
                var mainTeacherClasses = _classService.GetAllClassWithUserBeingMainTeacher(userId);
                var subTeacherClasses = _classService.GetAllClassWithUserBeingSubTeacher(userId);
                var studentClasses = _classService.GetAllClassWithUserBeingStudent(userId);
                var classesResponse = mainTeacherClasses
                    .Select(cl => new ClassShortInformationResponse(cl, "teacher", cl.MainTeacher)).ToList();
                classesResponse.AddRange(subTeacherClasses.Select(cl =>
                    new ClassShortInformationResponse(cl, "subteacher", cl.MainTeacher)));
                classesResponse.AddRange(studentClasses.Select(cl =>
                    new ClassShortInformationResponse(cl, "student", cl.MainTeacher)));

                classesResponse = classesResponse.OrderByDescending(cl => cl.StartDate).ToList();

                return Ok(classesResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddNewClass(AddNewClassRequest request)
        {
            var userId = GetCurrentUserId();

            // create new class

            var newAddClass = _classService.AddNewClass(request.Name, request.StartDate, request.Room,
                request.Description, userId);

            return Ok(new ClassShortInformationResponse(newAddClass, "teacher", newAddClass.MainTeacher));
        }

        [HttpGet("{classId}/assignment")]
        public IActionResult GetClassAssignments(int classId)
        {
            var foundList = _classService.GetClassAssignments(classId);
            if (foundList is null)
                return NotFound("Class not found");

            return Ok(foundList.Select(e => new AssignmentInformationResponse(e)));
        }

        [HttpPost("{classId}/assignment")]
        public IActionResult AddClassAssignment(int classId, AddAssignmentRequest request)
        {
            var userId = GetCurrentUserId();

            var foundClass = _classService.GetClassDetail(classId);
            if (foundClass is null)
                return BadRequest("Class does not exists");
            var userRoleInClass = _userServices.GetUserRoleInClass(userId, classId);
            // check if user is a teacher in class 
            if (userRoleInClass != 1)
                return Unauthorized("User not a teacher in class");

            var newAssignment = _classService.AddNewClassAssignment(classId, request.Name, request.Point);
            var response = new AssignmentInformationResponse(newAssignment);

            return Ok(response);
        }


        [HttpPut("{classId}/assignment/{assignmentId}")]
        public IActionResult UpdateClassAssignment(int classId, int assignmentId, [FromBody] UpdateClassRequest request)
        {
            var userId = GetCurrentUserId();
            var userRoleInClass = _userServices.GetUserRoleInClass(userId, classId);
            // check if user is a teacher in class 
            if (userRoleInClass != 1)
                return Unauthorized("User not a teacher in class");

            var updatedAssignment = _classService.UpdateClassAssignment(assignmentId, request.Name, request.Point);

            var response = new AssignmentInformationResponse(updatedAssignment);

            return Ok(response);
        }

        [HttpPut("{classId}/assignment/priority")]
        public IActionResult UpdateClassAssignmentPriority(int classId,
            [FromBody] UpdateAssignmentPriorityRequest request)
        {
            var userId = GetCurrentUserId();
            var userRoleInClass = _userServices.GetUserRoleInClass(userId, classId);
            // check if user is a teacher in class 
            if (userRoleInClass != 1)
                return Unauthorized("User not a teacher in class");

            var resultAssignmentList =
                _classService.UpdateClassAssignmentPriority(classId, request.AssignmentIdPriorityOrder);


            return Ok(resultAssignmentList.Select(a => new AssignmentInformationResponse(a)));
        }

        [HttpDelete("{classId}/assignment/{assignmentId}")]
        public IActionResult RemoveClassAssignment(int classId, int assignmentId)
        {
            var userId = GetCurrentUserId();
            var userRoleInClass = _userServices.GetUserRoleInClass(userId, classId);
            // check if user is a teacher in class 
            if (userRoleInClass != 1)
                return Unauthorized("User not a teacher in class");
            var result = _classService.RemoveAssignment(assignmentId);
            if (result) return Ok($"Assignment with Id {assignmentId} removed");

            return BadRequest("Error while removing");
        }

        private int GetCurrentUserId()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }
    }
}