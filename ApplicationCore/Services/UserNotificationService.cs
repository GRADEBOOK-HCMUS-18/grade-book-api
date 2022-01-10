using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class UserNotificationService: IUserNotificationService
    {
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IBaseRepository<UserNotification> _notificationRepository;
        private readonly IBaseRepository<User> _userRepository;


        public UserNotificationService(IBaseRepository<Assignment> assignmentRepository, IBaseRepository<UserNotification> notificationRepository, IBaseRepository<User> userRepository)
        {
            _assignmentRepository = assignmentRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }
        public List<UserNotification> ReadPagedUserNotification(int userId,int pageNumber, int numOfNotificationPerPage)
        {
            var listNotification = _notificationRepository
                .List(new UserNotificationWithUserIdAndPagingSpec(userId, numOfNotificationPerPage, pageNumber));

            return listNotification.ToList();

        }

        public void SetUserNotificationAsViewed(int userId)
        {
            var foundUser =
                _userRepository
                    .GetFirst(u => u.Id == userId,
                        u => u.Include(user => user.UserNotifications)); 
            foundUser.SetAllNotificationRead(true);
            _userRepository.Update(foundUser);
        }

        public void AddNewFinalizedGradeCompositionNotification(int assignmentId)
        {
            var foundAssignment = _assignmentRepository.GetFirst(a => a.Id == assignmentId,
                a => a.Include(ass => ass.Class)
                    .ThenInclude(c => c.ClassStudentsAccounts)
                    .ThenInclude(cs => cs.Student)
            );

            var newNotifications = (
                from studentAccount in foundAssignment.Class.ClassStudentsAccounts
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

        public UserNotification AddNewGradeRequestNotification(AssignmentGradeReviewRequest request)
        {
            throw new System.NotImplementedException();
        }

        public UserNotification AddNewGradeReviewReplyNotification(GradeReviewReply reply)
        {
            throw new System.NotImplementedException();
        }

        public UserNotification AddAcceptedOrRejectedGradeReviewNotification(AssignmentGradeReviewRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}