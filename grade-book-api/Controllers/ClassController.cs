using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using grade_book_api.Requests.ClassRequests;
using grade_book_api.Responses.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IUserServices _userServices;
        private readonly IAssignmentService _assignmentService;
        private readonly IReviewService _reviewService;
        private readonly IUserNotificationService _notificationService;

        public ClassController(
            IClassService classService,
            IUserServices userServices,
            IAssignmentService assignmentService,
            IReviewService reviewService, IUserNotificationService notificationService)
        {
            _classService = classService;
            _userServices = userServices;
            _assignmentService = assignmentService;
            _reviewService = reviewService;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("{classId}")]
        public IActionResult GetClassDetail(int classId)
        {
            try
            {
                var userRoleInClass = GetCurrentUserRole(classId);
                if (userRoleInClass is ClassRole.NotAMember) return Unauthorized("User not a member in class");
                var foundClass = _classService.GetClassDetail(classId);
                var response = new ClassDetailInformationResponse(foundClass, userRoleInClass == ClassRole.Teacher);

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
            var userId = GetCurrentUserIdFromToken();
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
            var userId = GetCurrentUserIdFromToken();

            // create new class

            var newAddClass = _classService.AddNewClass(request.Name, request.StartDate, request.Room,
                request.Description, userId);

            return Ok(new ClassShortInformationResponse(newAddClass, "teacher", newAddClass.MainTeacher));
        }

        [HttpGet("{classId}/assignment")]
        public IActionResult GetClassAssignments(int classId)
        {
            var foundList = _assignmentService.GetClassAssignments(classId);
            if (foundList is null)
                return NotFound("Class not found");

            return Ok(foundList.Select(e => new AssignmentInformationResponse(e)));
        }


        [HttpGet("{classId}/grade")]
        public IActionResult GetAllGradesWithAssignmentInClass(int classId)
        {
            var userId = GetCurrentUserIdFromToken();
            var currentUserRole = GetCurrentUserRole(classId);
            if (currentUserRole is ClassRole.Teacher)
            {
                var assignments = _assignmentService.GetAllClassAssignmentWithGradeAsTeacher(classId);
                var studentRecords = _classService.GetStudentListInClass(classId);
                var response = studentRecords.Select(s => new GradeBoardDetailTeacherResponse(s, assignments));
                return Ok(response);
            }

            if (currentUserRole is ClassRole.Student)
            {
                var studentRecord = _classService.GetStudentRecordOfUserInClass(userId, classId);
                var assignments = _assignmentService.GetAllClassAssignmentWithGradeAsStudent(classId, userId);
                var response = new GradeBoardDetailStudentResponse(studentRecord, assignments);
                return Ok(response);
            }

            return Unauthorized("User not a member in class");
        }

        [HttpPost("{classId}/assignment")]
        public IActionResult AddClassAssignment(int classId, AddAssignmentRequest request)
        {
            var foundClass = _classService.GetClassDetail(classId);
            if (foundClass is null)
                return BadRequest("Class does not exists");
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");

            var newAssignment = _assignmentService.AddNewClassAssignment(classId, request.Name, request.Point);
            var response = new AssignmentInformationResponse(newAssignment);

            return Ok(response);
        }


        [HttpPut("{classId}/assignment/{assignmentId}")]
        public IActionResult UpdateClassAssignment(int classId, int assignmentId, [FromBody] UpdateClassRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");

            var updatedAssignment = _assignmentService.UpdateClassAssignment(assignmentId, request.Name, request.Point);

            var response = new AssignmentInformationResponse(updatedAssignment);

            return Ok(response);
        }

        [HttpPut("{classId}/assignment/priority")]
        public IActionResult UpdateClassAssignmentPriority(int classId,
            [FromBody] UpdateAssignmentPriorityRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");

            var resultAssignmentList =
                _assignmentService.UpdateClassAssignmentPriority(classId, request.AssignmentIdPriorityOrder);


            return Ok(resultAssignmentList.Select(a => new AssignmentInformationResponse(a)));
        }

        [HttpDelete("{classId}/assignment/{assignmentId}")]
        public IActionResult RemoveClassAssignment(int classId, int assignmentId)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");
            var result = _assignmentService.RemoveAssignment(assignmentId);
            if (result) return Ok($"Assignment with Id {assignmentId} removed");

            return BadRequest("Error while removing");
        }

        [HttpPut("{classId}/assignment/{assignmentId}/finalization")]
        public IActionResult UpdateAssignmentFinalization(int classId, int assignmentId,
            UpdateAssignmentFinalizationRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");
            _ = _assignmentService.UpdateAssignmentFinalization(assignmentId, request.NewStatus);
            if (request.NewStatus)
            {
                _notificationService.AddNewFinalizedGradeCompositionNotification(assignmentId);
            }
            // TODO: insert new finalization notification 
            return Ok();
        }

        [HttpPut("{classId}/finalization")]
        public IActionResult UpdateWholeClassAssignmentFinalization(int classId,
            UpdateAssignmentFinalizationRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");
            _ = _assignmentService.UpdateWholeClassAssignmentFinalization(classId, request.NewStatus);
            // TODO: insert new finalization notification 
            return Ok();
        }


        // uploading class student 
        [HttpPost("{classId}/student")]
        public IActionResult BulkAddClassStudent(int classId, BulkAddStudentsToClassRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not teacher in class");
            var idNamePair = request.Students.Select(s =>
                new Tuple<string, string>(s.StudentId, s.FullName)
            ).ToList();
            var studentRecords = _classService.BulkAddStudentToClass(classId, idNamePair);
            if (studentRecords is null) return NotFound();
            return Ok(studentRecords.Select(student => new StudentRecordResponse(student)));
        }

        //uploading assignment grade
        [HttpPost("{classId}/assignment/{assignmentId}/grade")]
        public IActionResult BulkAddStudentAssignmentGrade(int classId, int assignmentId,
            BulkAddGradesToAssignmentRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not teacher in class");

            var idGradePairs = request.Grades
                .Select(g => new Tuple<string, int>(g.StudentId, g.Grade)).ToList();

            try
            {
                var result = _assignmentService.BulkAddStudentGradeToAssignment(assignmentId, idGradePairs);
                if (result is null) return NotFound();
                return Ok(result.Select(sGrade => new
                    {sGrade.StudentRecord.StudentIdentification, Grade = sGrade.Point, sGrade.IsFinalized}));
            }
            catch (ConstraintException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{classId}/assignment/{assignmentId}/grade")]
        public IActionResult UpdateSingleStudentGrade(int classId, int assignmentId,
            UpdateSingleStudentGradeRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("User not a teacher in class");
            var result = _assignmentService.UpdateStudentAssignmentGrade(assignmentId, request.StudentId,
                request.NewStatus,
                request.NewPoint);


            return Ok(new AssignmentGradeResponse(result));
        }

        [HttpGet("{classId}/review")]
        public IActionResult GetAllGradeReviewInClass(int classId)
        {
            var currentRole = GetCurrentUserRole(classId);
            if (currentRole is ClassRole.Teacher)
            {
                var reviews = _reviewService.GetReviewRequestsAsTeacher(classId);
                return Ok(reviews.Select(review => new GradeReviewInformationResponse(review))); 
            }

            if (currentRole is ClassRole.Student)
            {
                var currentUserStudentId = GetCurrentUserStudentId();
            
                var reviews = _reviewService.GetReviewRequestsAsStudent(classId,currentUserStudentId);
                return Ok(reviews.Select(review => new GradeReviewInformationResponse(review))); 
            }

            return Unauthorized(); 
        }

        [HttpPost("{classId}/assignment/{assignmentId}/review")]
        public IActionResult AddGradeViewRequest(int classId, int assignmentId, AddNewGradeReviewRequest request)
        {
            var currentUserRole = GetCurrentUserRole(classId);
            if (currentUserRole is not ClassRole.Student)
               return Unauthorized("Only student in class allowed");
            try
            {
                var user = _userServices.GetUserById(GetCurrentUserIdFromToken());
                if (string.IsNullOrEmpty(user.StudentIdentification))
                {
                   return Unauthorized("Only student with studentId is allowed to raise a grade review");
                }
                var newReview = _reviewService
                    .AddReviewRequest(assignmentId, 
                        GetCurrentUserIdFromToken(), 
                        request.RequestedNewPoint, 
                        request.Description);
                if (newReview is null) return BadRequest("No grade for student in class"); 
                return Ok(new GradeReviewInformationResponse(newReview));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        // change review state 
        [HttpPut("{classId}/review/{reviewId}")]
        public IActionResult ChangeGradeReviewState(int classId, int reviewId, UpdateGradeReviewStateRequest request)
        {
            if (GetCurrentUserRole(classId) is not ClassRole.Teacher)
                return Unauthorized("Only teacher is allowed to update state of a review request");
            var allowedState = new string[] {"waiting", "accepted", "rejected"};
            if (allowedState.Contains(request.State))
            {
                _reviewService.UpdateGradeReviewState(reviewId, request.State);
                // TODO: Insert new change review state notification
                return Ok("Updated");
            }

            return BadRequest("Allowed new states are: waiting, accepted, rejected"); 
        }

        [HttpPost("{classId}/review/{reviewId}")]
        public IActionResult AddGradeRequestReply(int classId, int reviewId, AddGradeRequestReplyRequest request)
        {
            var currentUserRole = GetCurrentUserRole(classId);
            var currentUserId = GetCurrentUserIdFromToken();

            if (currentUserRole is ClassRole.Teacher)
            {
                var result =
                    _reviewService.AddReviewReplyAsTeacher(currentUserId, reviewId, request.Content);
                // TODO: insert new grade request reply notification 
                return Ok(new ReviewReplyResponse(result));
            }

            if (currentUserRole is ClassRole.Student)
            {
                try
                {
                    var result = 
                        _reviewService.AddReviewReplyAsStudent(currentUserId, reviewId, request.Content);
                    return Ok(new ReviewReplyResponse(result));
                }
                catch (ApplicationException ex)
                {
                    return Unauthorized(ex.Message);
                }

            }
            return Unauthorized();
        }


        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }

        private ClassRole GetCurrentUserRole(int classId)
        {
            return _userServices.GetUserRoleInClass(GetCurrentUserIdFromToken(), classId);
        }

        private string GetCurrentUserStudentId()
        {
            var userId = GetCurrentUserIdFromToken();
            string currentStudentIdentification = _userServices.GetUserById(userId).StudentIdentification;
            return currentStudentIdentification;
        }
    }
}