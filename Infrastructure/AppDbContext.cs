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
        public DbSet<ClassStudents> ClassStudents { get; set; }
        public DbSet<ClassTeachers> ClassTeachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setting up class-user-classstudents
            // set up composite key
            modelBuilder.Entity<ClassStudents>().HasKey(cs => new {cs.ClassId, cs.StudentId});
            // set up one-many between ClassStudent and Class
            modelBuilder.Entity<ClassStudents>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassStudents)
                .HasForeignKey(cs => cs.ClassId);

            // set up one-many between ClassStudent and Student
            modelBuilder.Entity<ClassStudents>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.ClassStudents)
                .HasForeignKey(cs => cs.StudentId);


            // setting up class-user-classteachers

            modelBuilder.Entity<ClassTeachers>().HasKey(ct => new {ct.ClassId, ct.TeacherId});
            // set up one-many between ClassTeacher and Class
            modelBuilder.Entity<ClassTeachers>()
                .HasOne(ct => ct.Class)
                .WithMany(c => c.ClassTeachers)
                .HasForeignKey(ct => ct.ClassId);

            // set up one-many between ClassTeacher and Teacher
            modelBuilder.Entity<ClassTeachers>()
                .HasOne(ct => ct.Teacher)
                .WithMany(s => s.ClassTeachers)
                .HasForeignKey(ct => ct.TeacherId);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}