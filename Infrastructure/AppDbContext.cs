using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudentsAccount> ClassStudentsAccounts { get; set; }
        public DbSet<ClassTeachersAccount> ClassTeachersAccounts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentRecord> StudentsRecords { get; set; }
        public DbSet<StudentAssignmentGrade> StudentAssignmentGrades { get; set; }
        public DbSet<AssignmentGradeReviewRequest> AssignmentGradeReviewRequests { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<AccountConfirmationRequest> AccountConfirmationRequests { get; set; }
        public DbSet<PasswordChangeRequest> PasswordChangeRequests { get; set; }
        public DbSet<AdminAccount> AdminAccounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassStudentsAccount>().HasKey(cs => new {cs.ClassId, StudentId = cs.StudentAccountId});
            SetupStudentAccountAndClassRelationship(modelBuilder);

            modelBuilder.Entity<ClassTeachersAccount>().HasKey(ct => new {ct.ClassId, ct.TeacherId});
            SetupTeacherAccountAndClassRelationship(modelBuilder);


            //modelBuilder.Entity<StudentRecord>().HasKey(s => new {s.RecordId});
            SetupClassAndAssignmentRelationship(modelBuilder);
            SetupUserAndConfirmationRequestRelationship(modelBuilder);
            SetupUserAndPasswordChangeRequestRelationship(modelBuilder);
            SetupClassAndStudentRelationship(modelBuilder);
            SetupAssignmentAndStudentRelationship(modelBuilder);
            SetupStudentGradeAndReviewRequestRelationship(modelBuilder);
            SetupUserAndNotificationRelationship(modelBuilder);
            SetupNotificationAndClassRelationship(modelBuilder);
            SetupNotificationAndAssignmentRelationship(modelBuilder);
            SetupNotificationAndReviewRequestRelationship(modelBuilder);
            SetupAssignmentGradeReviewAndReplyRelationship(modelBuilder);
            SetupUserAndGradeReviewReplyRelationship(modelBuilder);
            SetupSeedData(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
        }

        private static void SetupSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User> { SeedData.SeedUser, SeedData.SeedUser2 });
            modelBuilder.Entity<AdminAccount>().HasData(SeedData.SeedAdmin);
            modelBuilder.Entity<Class>().HasData(SeedData.SeedClass);
            modelBuilder.Entity<ClassStudentsAccount>().HasData(SeedData.SeedClassStudentAccount);
            modelBuilder.Entity<ClassTeachersAccount>().HasData(SeedData.SeedClassTeacherAccount);
            modelBuilder.Entity<StudentRecord>().HasData(SeedData.SeedStudentRecord);
            modelBuilder.Entity<Assignment>().HasData(SeedData.SeedAssignment);
            modelBuilder.Entity<StudentAssignmentGrade>().HasData(SeedData.SeedStudentAssignmentGrade);
            modelBuilder.Entity<AssignmentGradeReviewRequest>().HasData(SeedData.SeedReviewRequest);
            modelBuilder.Entity<GradeReviewReply>().HasData(SeedData.SeedReviewReply);
        }

        

        private static void SetupClassAndAssignmentRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().HasOne(a => a.Class)
                .WithMany(c => c.ClassAssignments)
                .HasForeignKey(a => a.ClassId);
        }
        private static void SetupUserAndConfirmationRequestRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountConfirmationRequest>()
                .HasOne(confirm => confirm.User)
                .WithMany(user => user.AccountConfirmationRequests)
                .HasForeignKey(confirm => confirm.UserId);
        }

        private static void SetupUserAndPasswordChangeRequestRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PasswordChangeRequest>()
                .HasOne(p => p.User)
                .WithMany(user => user.PasswordChangeRequests)
                .HasForeignKey(p => p.UserId);
        }

        private static void SetupAssignmentAndStudentRelationship(ModelBuilder modelBuilder)
        {
            // setup assignment-student-studentassignmentgrade
            modelBuilder.Entity<StudentAssignmentGrade>()
                .HasOne(sGrade => sGrade.Assignment)
                .WithMany(assignment => assignment.StudentAssignmentGrades)
                .HasForeignKey(sGrade => sGrade.AssignmentId);
            modelBuilder.Entity<StudentAssignmentGrade>()
                .HasOne(sGrade => sGrade.StudentRecord)
                .WithMany(student => student.Grades)
                .HasForeignKey(sGrade => sGrade.StudentRecordId);
        }

        private static void SetupClassAndStudentRelationship(ModelBuilder modelBuilder)
        {
            // set up one-many between Student and Class

            modelBuilder.Entity<StudentRecord>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(cs => cs.ClassId);
        }

        private static void SetupUserAndNotificationRelationship(ModelBuilder modelBuilder)
        {
            // set-up one-many between User and Notifications 
            modelBuilder.Entity<UserNotification>()
                .HasOne(n => n.User)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(n => n.UserId);
        }

        private static void SetupNotificationAndClassRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotification>()
                .HasOne(n => n.Class)
                .WithMany(c => c.UserNotifications)
                .HasForeignKey(n => n.ClassId);
        }
        private static void SetupNotificationAndAssignmentRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotification>()
                .HasOne(n => n.Assignment)
                .WithMany(a => a.UserNotifications)
                .HasForeignKey(n => n.AssignmentId);
        }   
        private static void SetupNotificationAndReviewRequestRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotification>()
                .HasOne(n => n.AssignmentGradeReviewRequest);
        }   

        private static void SetupAssignmentGradeReviewAndReplyRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GradeReviewReply>()
                .HasOne(reply => reply.AssignmentGradeReviewRequest)
                .WithMany(r => r.GradeReviewReplies)
                .HasForeignKey(reply => reply.AssignmentGradeReviewRequestId);
        }

        private static void SetupUserAndGradeReviewReplyRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GradeReviewReply>()
                .HasOne(reply => reply.Replier)
                .WithMany(u => u.GradeReviewReplies)
                .HasForeignKey(reply => reply.ReplierId);
        }
        

        private static void SetupStudentGradeAndReviewRequestRelationship(ModelBuilder modelBuilder)
        {
            // set up one-many between Student Grade and and Review Request
            modelBuilder.Entity<AssignmentGradeReviewRequest>()
                .HasOne(r => r.StudentAssignmentGrade)
                .WithMany(sGrade => sGrade.AssignmentGradeReviewRequests)
                .HasForeignKey(r => r.StudentAssignmentGradeId);
        }

        private static void SetupTeacherAccountAndClassRelationship(ModelBuilder modelBuilder)
        {
            // setting up class-user-classteachers

            // set up one-many between ClassTeacher and Class
            modelBuilder.Entity<ClassTeachersAccount>()
                .HasOne(ct => ct.Class)
                .WithMany(c => c.ClassTeachersAccounts)
                .HasForeignKey(ct => ct.ClassId);

            // set up one-many between ClassTeacher and Teacher
            modelBuilder.Entity<ClassTeachersAccount>()
                .HasOne(ct => ct.Teacher)
                .WithMany(s => s.ClassTeachersAccounts)
                .HasForeignKey(ct => ct.TeacherId);
        }

        private static void SetupStudentAccountAndClassRelationship(ModelBuilder modelBuilder)
        {
            // setting up class-user-classstudents
            // set up composite key
            // set up one-many between ClassStudent and Class
            modelBuilder.Entity<ClassStudentsAccount>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassStudentsAccounts)
                .HasForeignKey(cs => cs.ClassId);

            // set up one-many between ClassStudent and Student
            modelBuilder.Entity<ClassStudentsAccount>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.ClassStudentsAccounts)
                .HasForeignKey(cs => cs.StudentAccountId);
        }
    }
}