﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220115072853_datetime-admin-account")]
    partial class datetimeadminaccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ApplicationCore.Entity.AccountConfirmationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConfirmationCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountConfirmationRequests");
                });

            modelBuilder.Entity("ApplicationCore.Entity.AdminAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AdminAccounts");

                    b.HasData(
                        new
                        {
                            Id = 2022,
                            DateCreated = new DateTime(2022, 1, 15, 14, 28, 53, 96, DateTimeKind.Local).AddTicks(1743),
                            IsSuperAdmin = true,
                            PasswordHash = new byte[] { 215, 205, 237, 201, 28, 18, 93, 8, 10, 16, 144, 242, 160, 219, 186, 70, 1, 46, 234, 143, 79, 160, 165, 29, 111, 34, 179, 109, 58, 20, 38, 110, 134, 138, 22, 254, 229, 155, 128, 47, 107, 192, 30, 88, 144, 64, 241, 159, 5, 54, 74, 148, 8, 219, 252, 84, 78, 236, 4, 214, 0, 159, 173, 204 },
                            PasswordSalt = new byte[] { 154, 53, 121, 42, 89, 155, 156, 17, 82, 178, 34, 233, 155, 244, 54, 223, 66, 253, 162, 242, 122, 99, 169, 137, 9, 198, 192, 5, 45, 16, 36, 200, 184, 45, 249, 23, 207, 24, 194, 179, 209, 229, 137, 97, 136, 138, 68, 179, 231, 237, 188, 27, 9, 135, 66, 150, 157, 36, 5, 150, 132, 169, 171, 121, 145, 150, 211, 179, 227, 140, 77, 7, 119, 181, 136, 210, 25, 237, 247, 217, 107, 175, 241, 129, 222, 187, 195, 233, 175, 61, 108, 129, 86, 97, 155, 11, 239, 174, 37, 253, 214, 15, 250, 71, 194, 198, 157, 232, 35, 204, 87, 134, 94, 254, 102, 161, 134, 167, 73, 211, 34, 6, 97, 192, 236, 12, 150, 239 },
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entity.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ApplicationCore.Entity.AssignmentGradeReviewRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("RequestState")
                        .HasColumnType("integer");

                    b.Property<int>("RequestedNewPoint")
                        .HasColumnType("integer");

                    b.Property<int>("StudentAssignmentGradeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentAssignmentGradeId");

                    b.ToTable("AssignmentGradeReviewRequests");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("InviteStringStudent")
                        .HasColumnType("text");

                    b.Property<string>("InviteStringTeacher")
                        .HasColumnType("text");

                    b.Property<int?>("MainTeacherId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Room")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MainTeacherId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ApplicationCore.Entity.ClassStudentsAccount", b =>
                {
                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentAccountId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("ClassId", "StudentAccountId");

                    b.HasIndex("StudentAccountId");

                    b.ToTable("ClassStudentsAccounts");
                });

            modelBuilder.Entity("ApplicationCore.Entity.ClassTeachersAccount", b =>
                {
                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("ClassId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassTeachersAccounts");
                });

            modelBuilder.Entity("ApplicationCore.Entity.GradeReviewReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AssignmentGradeReviewRequestId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ReplierId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentGradeReviewRequestId");

                    b.HasIndex("ReplierId");

                    b.ToTable("GradeReviewReply");
                });

            modelBuilder.Entity("ApplicationCore.Entity.PasswordChangeRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConfirmationCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordChangeRequests");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentAssignmentGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AssignmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFinalized")
                        .HasColumnType("boolean");

                    b.Property<int>("Point")
                        .HasColumnType("integer");

                    b.Property<int>("StudentRecordId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("StudentRecordId");

                    b.ToTable("StudentAssignmentGrades");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("StudentIdentification")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("StudentsRecords");
                });

            modelBuilder.Entity("ApplicationCore.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DefaultProfilePictureHex")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPasswordNotSet")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text");

                    b.Property<string>("StudentIdentification")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApplicationCore.Entity.UserNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AssignmentGradeReviewRequestId")
                        .HasColumnType("integer");

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("integer");

                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsViewed")
                        .HasColumnType("boolean");

                    b.Property<int>("NotificationType")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentGradeReviewRequestId");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("ClassId");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("ApplicationCore.Entity.AccountConfirmationRequest", b =>
                {
                    b.HasOne("ApplicationCore.Entity.User", "User")
                        .WithMany("AccountConfirmationRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Assignment", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Class", "Class")
                        .WithMany("ClassAssignments")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ApplicationCore.Entity.AssignmentGradeReviewRequest", b =>
                {
                    b.HasOne("ApplicationCore.Entity.StudentAssignmentGrade", "StudentAssignmentGrade")
                        .WithMany("AssignmentGradeReviewRequests")
                        .HasForeignKey("StudentAssignmentGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentAssignmentGrade");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Class", b =>
                {
                    b.HasOne("ApplicationCore.Entity.User", "MainTeacher")
                        .WithMany()
                        .HasForeignKey("MainTeacherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("MainTeacher");
                });

            modelBuilder.Entity("ApplicationCore.Entity.ClassStudentsAccount", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Class", "Class")
                        .WithMany("ClassStudentsAccounts")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entity.User", "Student")
                        .WithMany("ClassStudentsAccounts")
                        .HasForeignKey("StudentAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ApplicationCore.Entity.ClassTeachersAccount", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Class", "Class")
                        .WithMany("ClassTeachersAccounts")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entity.User", "Teacher")
                        .WithMany("ClassTeachersAccounts")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ApplicationCore.Entity.GradeReviewReply", b =>
                {
                    b.HasOne("ApplicationCore.Entity.AssignmentGradeReviewRequest", "AssignmentGradeReviewRequest")
                        .WithMany("GradeReviewReplies")
                        .HasForeignKey("AssignmentGradeReviewRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entity.User", "Replier")
                        .WithMany("GradeReviewReplies")
                        .HasForeignKey("ReplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignmentGradeReviewRequest");

                    b.Navigation("Replier");
                });

            modelBuilder.Entity("ApplicationCore.Entity.PasswordChangeRequest", b =>
                {
                    b.HasOne("ApplicationCore.Entity.User", "User")
                        .WithMany("PasswordChangeRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentAssignmentGrade", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Assignment", "Assignment")
                        .WithMany("StudentAssignmentGrades")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entity.StudentRecord", "StudentRecord")
                        .WithMany("Grades")
                        .HasForeignKey("StudentRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("StudentRecord");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentRecord", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ApplicationCore.Entity.UserNotification", b =>
                {
                    b.HasOne("ApplicationCore.Entity.AssignmentGradeReviewRequest", "AssignmentGradeReviewRequest")
                        .WithMany()
                        .HasForeignKey("AssignmentGradeReviewRequestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Entity.Assignment", "Assignment")
                        .WithMany("UserNotifications")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Entity.Class", "Class")
                        .WithMany("UserNotifications")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entity.User", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("AssignmentGradeReviewRequest");

                    b.Navigation("Class");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Assignment", b =>
                {
                    b.Navigation("StudentAssignmentGrades");

                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("ApplicationCore.Entity.AssignmentGradeReviewRequest", b =>
                {
                    b.Navigation("GradeReviewReplies");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Class", b =>
                {
                    b.Navigation("ClassAssignments");

                    b.Navigation("ClassStudentsAccounts");

                    b.Navigation("ClassTeachersAccounts");

                    b.Navigation("Students");

                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentAssignmentGrade", b =>
                {
                    b.Navigation("AssignmentGradeReviewRequests");
                });

            modelBuilder.Entity("ApplicationCore.Entity.StudentRecord", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("ApplicationCore.Entity.User", b =>
                {
                    b.Navigation("AccountConfirmationRequests");

                    b.Navigation("ClassStudentsAccounts");

                    b.Navigation("ClassTeachersAccounts");

                    b.Navigation("GradeReviewReplies");

                    b.Navigation("PasswordChangeRequests");

                    b.Navigation("UserNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
