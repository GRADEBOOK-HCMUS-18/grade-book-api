using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IBaseRepository<UserNotification> _notificationRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<AssignmentGradeReviewRequest> _reviewRepository;
        private readonly IBaseRepository<Class> _classRepository;
        private readonly IBaseRepository<GradeReviewReply> _replyRepository;


        public UserNotificationService(IBaseRepository<Assignment> assignmentRepository,
            IBaseRepository<UserNotification> notificationRepository, IBaseRepository<User> userRepository,
            IBaseRepository<AssignmentGradeReviewRequest> reviewRepository, IBaseRepository<Class> classRepository,
            IBaseRepository<GradeReviewReply> replyRepository)
        {
            _assignmentRepository = assignmentRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _classRepository = classRepository;
            _replyRepository = replyRepository;
        }

        public List<UserNotification> ReadPagedUserNotification(int userId, int pageNumber,
            int numOfNotificationPerPage)
        {
            var listNotification = _notificationRepository
                .List(new UserNotificationWithUserIdAndPagingSpec(userId, numOfNotificationPerPage, pageNumber));

            return listNotification.ToList();
        }

        public void SetSingleUserNotificationAsViewed(int notificationId)
        {
            var foundNotification = _notificationRepository.GetFirst(n => n.Id == notificationId);
            foundNotification.IsViewed = true;

            _notificationRepository.Update(foundNotification);

        }

        public void SetAllUserNotificationAsViewed(int userId)
        {
            var foundUser =
                _userRepository
                    .GetFirst(u => u.Id == userId,
                        u => u.Include(user => user.UserNotifications));
            foundUser.SetAllNotificationRead(true);
            _userRepository.Update(foundUser);
        }

        public int CountNotViewedNotification(int userId)
        {
            return _notificationRepository.Count(n => !n.IsViewed && n.UserId == userId);
        }

        public void AddNewFinalizedGradeCompositionNotification(int assignmentId)
        {
            var foundAssignment = _assignmentRepository.GetFirst(a => a.Id == assignmentId,
                a => a.Include(ass => ass.Class)
                    .ThenInclude(c => c.ClassStudentsAccounts)
                    .ThenInclude(cs => cs.Student)
            );

            var studentAccounts = foundAssignment.Class.ClassStudentsAccounts;

            var newNotifications = (
                from studentAccount in studentAccounts // to all student account in class 
                select studentAccount.Student
                into user
                where !string.IsNullOrEmpty(user.StudentIdentification)
                select new UserNotification
                {
                    NotificationType = NotificationType.NewFinalizedGradeComposition,
                    Assignment = foundAssignment,
                    AssignmentId = foundAssignment.Id,
                    Class = foundAssignment.Class,
                    ClassId = foundAssignment.Class.Id,
                    AssignmentGradeReviewRequest = null,
                    User = user,
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    IsViewed = false
                }).ToList();

            _notificationRepository.InsertRange(newNotifications);
        }

        public void AddNewGradeRequestNotification(int requestId)
        {
            var foundRequest = _reviewRepository.GetFirst(r => r.Id == requestId,
                r =>
                    r.Include(re => re.StudentAssignmentGrade)
                        .ThenInclude(sGrade => sGrade.Assignment)
                        .ThenInclude(a => a.Class)
            );

            var classOfRequest =
                _classRepository.GetFirst(c => c.Id == foundRequest.StudentAssignmentGrade.Assignment.Class.Id,
                    c => c.Include(cl => cl.MainTeacher)
                        .Include(cl => cl.ClassTeachersAccounts)
                        .ThenInclude(ct => ct.Teacher));

            var teachersList = classOfRequest.GetAllTeacher();

            var newNotifications = teachersList.Select(teacherAccount => new UserNotification
                {
                    NotificationType = NotificationType.NewGradeReviewRequest,
                    Assignment = foundRequest.StudentAssignmentGrade.Assignment,
                    AssignmentId = foundRequest.StudentAssignmentGrade.Assignment.Id,
                    Class = classOfRequest,
                    ClassId = classOfRequest.Id,
                    AssignmentGradeReviewRequest = foundRequest,
                    User = teacherAccount,
                    UserId = teacherAccount.Id
                })
                .ToList();

            _notificationRepository.InsertRange(newNotifications);
        }

        public void AddNewGradeReviewReplyNotification(int replyId)
        {
            var foundReply = _replyRepository.GetFirst(reply => reply.Id == replyId,
                reply => reply
                    .Include(r => r.AssignmentGradeReviewRequest)
                    .ThenInclude(request => request.StudentAssignmentGrade)
                    .ThenInclude(sGrade => sGrade.Assignment)
                    .ThenInclude(a => a.Class)
                    .Include(r => r.AssignmentGradeReviewRequest)
                    .ThenInclude(request => request.StudentAssignmentGrade)
                    .ThenInclude(sGrade => sGrade.StudentRecord));
            var foundStudentRecord = foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.StudentRecord;
            var foundStudent =
                _userRepository.GetFirst(u => u.StudentIdentification == foundStudentRecord.StudentIdentification);


            var classOfRequest =
                _classRepository.GetFirst(
                    c => c.Id == foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.Assignment.Class.Id,
                    c => c.Include(cl => cl.MainTeacher)
                        .Include(cl => cl.ClassTeachersAccounts)
                        .ThenInclude(ct => ct.Teacher));

            var teachersList = classOfRequest.GetAllTeacher();

            var userReceive = teachersList;


            if (foundStudent is not null)
                userReceive.Add(foundStudent);
            // issuer does not receive a notification
            userReceive = userReceive.Where(u => u.Id != foundReply.ReplierId).ToList();


            var newNotifications = userReceive.Select(user => new UserNotification
            {
                NotificationType = NotificationType.NewGradeReviewReply,
                Assignment = foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.Assignment,
                AssignmentId = foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.AssignmentId,
                Class = foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.Assignment.Class,
                ClassId = foundReply.AssignmentGradeReviewRequest.StudentAssignmentGrade.Assignment.Class.Id,
                AssignmentGradeReviewRequest = foundReply.AssignmentGradeReviewRequest,
                User = user,
                UserId = user.Id
            }).ToList();

            _notificationRepository.InsertRange(newNotifications);
        }

        public void AddAcceptedOrRejectedGradeReviewNotification(int requestId)
        {
            var foundRequest = _reviewRepository.GetFirst(r => r.Id == requestId,
                r =>
                    r.Include(re => re.StudentAssignmentGrade)
                        .ThenInclude(sGrade => sGrade.Assignment)
                        .ThenInclude(a => a.Class)
                        .Include(re => re.StudentAssignmentGrade)
                        .ThenInclude(sGrade => sGrade.StudentRecord)
            );

            string studentIdentification = foundRequest.StudentAssignmentGrade.StudentRecord.StudentIdentification;
            var foundStudent = _userRepository.GetFirst(u => u.StudentIdentification == studentIdentification);
            if (foundStudent is null)
                return;
            var newNotification = new UserNotification
            {
                NotificationType = NotificationType.AcceptedOrRejectedGradeReview,
                Assignment = foundRequest.StudentAssignmentGrade.Assignment,
                AssignmentId = foundRequest.StudentAssignmentGrade.Assignment.Id,
                Class = foundRequest.StudentAssignmentGrade.Assignment.Class,
                ClassId = foundRequest.StudentAssignmentGrade.Assignment.Class.Id,
                AssignmentGradeReviewRequest = foundRequest,
                User = foundStudent,
                UserId = foundStudent.Id
            };

            _notificationRepository.Insert(newNotification);
        }
    }
}