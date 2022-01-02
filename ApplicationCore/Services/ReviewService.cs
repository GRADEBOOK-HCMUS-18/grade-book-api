using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ApplicationCore.Services
{
    public class ReviewService: IReviewService
    {
        private readonly IBaseRepository<AssignmentGradeReviewRequest> _reviewRepository;
        private readonly IBaseRepository<Class> _classRepository;
        private readonly IBaseRepository<StudentAssignmentGrade> _sGradeRepository;
        private readonly IBaseRepository<User> _userRepository;


        public ReviewService(IBaseRepository<AssignmentGradeReviewRequest> reviewRepository, 
                             IBaseRepository<Class> classRepository,
                             IBaseRepository<StudentAssignmentGrade> sGradeRepository,
                             IBaseRepository<User> userRepository)
        {
            _reviewRepository = reviewRepository;
            _classRepository = classRepository;
            _sGradeRepository = sGradeRepository;
            _userRepository = userRepository;
        }
        public AssignmentGradeReviewRequest AddReviewRequest(int assignmentId,int userId, int requestedPoint,
            string description)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            if (string.IsNullOrEmpty(foundUser.StudentIdentification))
            {
                throw new ApplicationException("Only student with student Id is allowed");
            }
            var foundSGrade = _sGradeRepository.GetFirst(
                sg => sg.StudentRecord.StudentIdentification == foundUser.StudentIdentification &&
                      sg.AssignmentId == assignmentId,
                sg => sg.Include(sGrade => sGrade.StudentRecord));

            if (foundSGrade is null)
                return null;
            var newReview = new AssignmentGradeReviewRequest
            {
                Description = description,
                StudentAssignmentGradeId = foundSGrade.Id,
                StudentAssignmentGrade = foundSGrade,
                RequestState = ReviewRequestState.Waiting,
                RequestedNewPoint = requestedPoint
            };


            return _reviewRepository.Insert(newReview);
        }

        public List<AssignmentGradeReviewRequest> GetReviewRequestsAsTeacher(int classId)
        {
            var foundSGrades = _sGradeRepository.List(
                sg => sg.Assignment.Class.Id == classId,
                null,
                sg => sg.Include(sGrade => sGrade.AssignmentGradeReviewRequests)
                    .Include(sGrade => sGrade.StudentRecord)
                    .Include(sGrade => sGrade.Assignment)
                    .ThenInclude(a => a.Class)
                ).ToList();
            
            var reviews = foundSGrades.SelectMany(sGrade => sGrade.AssignmentGradeReviewRequests);
            return reviews.ToList(); 

        }

        public List<AssignmentGradeReviewRequest> GetReviewRequestsAsStudent(int classId, string studentIdentification)
        {
            var foundSGrades = _sGradeRepository.List(
                sg => sg.Assignment.Class.Id == classId &&
                      sg.StudentRecord.StudentIdentification == studentIdentification,
                null,
                sg => sg.Include(sGrade => sGrade.AssignmentGradeReviewRequests)
                    .Include(sGrade => sGrade.StudentRecord)
                    .Include(sGrade => sGrade.Assignment)
                    .ThenInclude(a => a.Class)
            ).ToList();
            var reviews = foundSGrades.SelectMany(sGrade => sGrade.AssignmentGradeReviewRequests);
            return reviews.ToList(); 
        }
    }
}