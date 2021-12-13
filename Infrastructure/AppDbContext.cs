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
        public DbSet<Student> Students { get; set; }

        public DbSet<StudentAssignmentGrade> StudentAssignmentGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setting up class-user-classstudents
            // set up composite key
            modelBuilder.Entity<ClassStudentsAccount>().HasKey(cs => new {cs.ClassId, StudentId = cs.StudentAccountId});
            // set up one-many between ClassStudent and Class
            modelBuilder.Entity<ClassStudentsAccount>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassStudents)
                .HasForeignKey(cs => cs.ClassId);

            // set up one-many between ClassStudent and Student
            modelBuilder.Entity<ClassStudentsAccount>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.ClassStudentsAccounts)
                .HasForeignKey(cs => cs.StudentAccountId);


            // setting up class-user-classteachers

            modelBuilder.Entity<ClassTeachersAccount>().HasKey(ct => new {ct.ClassId, ct.TeacherId});
            // set up one-many between ClassTeacher and Class
            modelBuilder.Entity<ClassTeachersAccount>()
                .HasOne(ct => ct.Class)
                .WithMany(c => c.ClassTeachers)
                .HasForeignKey(ct => ct.ClassId);

            // set up one-many between ClassTeacher and Teacher
            modelBuilder.Entity<ClassTeachersAccount>()
                .HasOne(ct => ct.Teacher)
                .WithMany(s => s.ClassTeachersAccounts)
                .HasForeignKey(ct => ct.TeacherId);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            
            // set up composite key for Student table 
            modelBuilder.Entity<Student>().HasKey(s => new {s.ClassId, s.StudentIdentification});
            
            // set up one-many between Student and Class

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(cs => cs.ClassId); 
        }
    }
}