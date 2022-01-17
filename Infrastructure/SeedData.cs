using System;
using System.Collections.Generic;
using ApplicationCore.Entity;

namespace Infrastructure
{
    public static class SeedData
    {
        public static readonly User SeedUser =
            new(2023, "vothienan306.work@outlook.com", "Kieu", "Nguyen Hoang", "vothienan", "", "");

        public static User SeedUser2 = new(2024,
            "themasteroftherain@protonmail.com",
            "Hoang",
            "Vo Xuan",
            "vothienan",
            "",
            ""
        );

        public static readonly AdminAccount SeedAdmin = new(5024,
            "admin", "admin123", true
        );


        public static Class SeedClass = new()
        {
            Id = 2024,
            Name = "Math for the blind",

            StartDate = DateTime.Now,
            Room = "F201",
            Description = "Math for the blind, you don't need an eye",
            InviteStringStudent = "ABCDEFGHIKLM",
            InviteStringTeacher = "KJHGFDAQWERT",
            MainTeacher = null
        };

        public static ClassTeachersAccount SeedClassTeacherAccount = new()
        {
            Id = 2024,
            ClassId = 2024,
            TeacherId = 2023
        };

        public static ClassStudentsAccount SeedClassStudentAccount = new()
        {
            Id = 2024,
            ClassId = 2024,
            StudentAccountId = 2024
        };

        public static readonly StudentRecord SeedStudentRecord = new()
        {
            Id = 2024,
            ClassId = 2024,
            FullName = "Xuan Hoang Vo",
            StudentIdentification = "5037"
        };



        public static readonly Assignment SeedAssignment = new()
        {
            Id = 2024,
            ClassId = 2024,
            Name = "Assignment 1 for the blind",
            Priority = 100,
            Weight = 4
        };
        public static readonly StudentAssignmentGrade SeedStudentAssignmentGrade = new()
        {
            Id = 2024,
            AssignmentId = 2024,
            IsFinalized = false,
            StudentRecordId = 2024
        };


        public static readonly AssignmentGradeReviewRequest SeedReviewRequest = new()
        {
            Id = 2024,
            DateCreated = DateTime.Now,
            Description = "I want to raise to point to 100",
            RequestState = ReviewRequestState.Waiting,
            RequestedNewPoint = 100,
            StudentAssignmentGradeId = 2024
        };

        public static readonly GradeReviewReply SeedReviewReply = new()
        {
            Id = 2024,
            AssignmentGradeReviewRequestId = 2024,
            Content = "Ok I will take a look",
            ReplierId = 2023
        };
    }
}