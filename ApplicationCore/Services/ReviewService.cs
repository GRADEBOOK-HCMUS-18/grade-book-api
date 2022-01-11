using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;


namespace ApplicationCore.Services
{
    public class ReviewService: IReviewService
    {
        private readonly IBaseRepository<AssignmentGradeReviewRequest> _reviewRepository;
        private readonly IBaseRepository<Class> _classRepository;
        private readonly IBaseRepository<StudentAssignmentGrade> _sGradeRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<GradeReviewReply> _replyRepository;


        public ReviewService(IBaseRepository<AssignmentGradeReviewRequest> reviewRepository, 
                             IBaseRepository<Class> classRepository,
                             IBaseRepository<StudentAssignmentGrade> sGradeRepository,
                             IBaseRepository<User> userRepository,
                             IBaseRepository<GradeReviewReply> replyRepository)
        {
            _reviewRepository = reviewRepository;
            _classRepository = classRepository;
            _sGradeRepository = sGradeRepository;
            _userRepository = userRepository;
            _replyRepository = replyRepository;
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
                new StudentGradeWithAssignmentAndStudentSpec(assignmentId,foundUser.StudentIdentification));

            if (foundSGrade is null)
                return null;
            var newReview = new AssignmentGradeReviewRequest
            {
                Description = description,
                StudentAssignmentGradeId = foundSGrade.Id,
                StudentAssignmentGrade = foundSGrade,
                RequestState = ReviewRequestState.Waiting,
                RequestedNewPoint = requestedPoint,
                DateCreated = DateTime.Now
            };


            return _reviewRepository.Insert(newReview);
        }

        public List<AssignmentGradeReviewRequest> GetReviewRequestsAsTeacher(int classId)
        {
            var foundSGrades = _sGradeRepository.List(
                new StudentGradeWithClassSpec(classId)
                ).ToList();
            
            var reviews = foundSGrades.SelectMany(sGrade => sGrade.AssignmentGradeReviewRequests);
            return reviews.ToList(); 

        }

        public List<AssignmentGradeReviewRequest> GetReviewRequestsAsStudent(int classId, string studentIdentification)
        {
            var foundSGrades = _sGradeRepository.List(
                    new StudentGradeWithClassSpec(classId,studentIdentification)
            ).ToList();
            var reviews = foundSGrades.SelectMany(sGrade => sGrade.AssignmentGradeReviewRequests);
            return reviews.ToList(); 
        }

        public GradeReviewReply AddReviewReplyAsTeacher(int userId, int reviewId, string content)
        {
            
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            var foundRequest = _reviewRepository.GetFirst(r => r.Id == reviewId);

            var newReply = new GradeReviewReply()
            {
                Content = content,
                AssignmentGradeReviewRequestId = reviewId,
                AssignmentGradeReviewRequest = foundRequest,
                Replier = foundUser,
                ReplierId = userId,
                DateTime = DateTime.Now
            };

            return _replyRepository.Insert(newReply);
        }

        public GradeReviewReply AddReviewReplyAsStudent(int userId, int reviewId, string content)
        {
            var foundUser = _userRepository.GetFirst(user => user.Id == userId);
            var foundRequest = _reviewRepository.GetFirst(r => r.Id == reviewId, 
                r => r.Include(req => req.StudentAssignmentGrade)
                    .ThenInclude(sGrade => sGrade.StudentRecord));

            if (foundUser.StudentIdentification !=
                foundRequest.StudentAssignmentGrade.StudentRecord.StudentIdentification)
                throw new ApplicationException("Student is not issuer of this request");
            var newReply = new GradeReviewReply()
            {
                Content = content,
                AssignmentGradeReviewRequestId = reviewId,
                AssignmentGradeReviewRequest = foundRequest,
                Replier = foundUser,
                ReplierId = userId,
                DateTime = DateTime.Now
            };

            return _replyRepository.Insert(newReply);

        }

        public void UpdateGradeReviewState(int reviewId, string newState)
        {
            var foundRequest = _reviewRepository.GetFirst(r => r.Id == reviewId,
                r => r.Include(request => request.StudentAssignmentGrade));
            switch (newState)
            {
                case "accepted": {
                    foundRequest.Accept();
                    break;
                }
                case "rejected":
                {
                    foundRequest.Reject();
                    break;
                }
                case "waiting":
                {
                    foundRequest.Waiting();
                    break;
                }
            }

            _reviewRepository.Update(foundRequest);

        }
    }
}