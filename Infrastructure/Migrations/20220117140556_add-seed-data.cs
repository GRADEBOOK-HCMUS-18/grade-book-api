using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdminAccounts",
                columns: new[] { "Id", "DateCreated", "IsSuperAdmin", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 5024, new DateTime(2022, 1, 17, 21, 5, 55, 266, DateTimeKind.Local).AddTicks(3225), true, new byte[] { 243, 165, 167, 186, 5, 32, 181, 209, 186, 79, 82, 56, 227, 32, 140, 166, 160, 85, 192, 34, 165, 88, 224, 72, 142, 47, 94, 34, 197, 69, 249, 44, 13, 44, 239, 246, 97, 154, 168, 158, 218, 161, 26, 65, 231, 207, 92, 108, 207, 116, 109, 42, 9, 250, 124, 5, 255, 162, 79, 187, 16, 180, 83, 140 }, new byte[] { 194, 1, 65, 44, 198, 150, 35, 229, 252, 233, 3, 22, 67, 90, 106, 109, 213, 159, 64, 118, 184, 134, 5, 134, 1, 87, 77, 189, 151, 68, 29, 22, 219, 88, 205, 9, 186, 186, 46, 235, 15, 202, 138, 205, 203, 174, 159, 66, 65, 250, 247, 6, 145, 59, 52, 240, 94, 57, 29, 220, 25, 86, 55, 185, 127, 169, 168, 106, 24, 242, 61, 227, 208, 238, 241, 94, 250, 233, 56, 154, 108, 230, 127, 91, 243, 108, 81, 2, 137, 146, 47, 248, 62, 91, 171, 60, 49, 148, 219, 116, 98, 89, 210, 157, 61, 171, 158, 112, 28, 244, 229, 164, 134, 85, 6, 116, 207, 100, 39, 3, 140, 189, 11, 75, 247, 88, 196, 230 }, "admin" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Description", "InviteStringStudent", "InviteStringTeacher", "MainTeacherId", "Name", "Room", "StartDate" },
                values: new object[] { 2024, "Math for the blind, you don't need an eye", "ABCDEFGHIKLM", "KJHGFDAQWERT", null, "Math for the blind", "F201", new DateTime(2022, 1, 17, 21, 5, 55, 266, DateTimeKind.Local).AddTicks(8346) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "DefaultProfilePictureHex", "Email", "FirstName", "IsEmailConfirmed", "IsLocked", "IsPasswordNotSet", "LastName", "PasswordHash", "PasswordSalt", "ProfilePictureUrl", "StudentIdentification" },
                values: new object[,]
                {
                    { 2023, new DateTime(2022, 1, 17, 21, 5, 55, 260, DateTimeKind.Local).AddTicks(3794), "", "vothienan306.work@outlook.com", "Kieu", false, false, false, "Nguyen Hoang", new byte[] { 63, 96, 155, 11, 90, 127, 2, 107, 227, 117, 99, 21, 138, 123, 141, 15, 52, 73, 218, 32, 88, 91, 92, 123, 8, 175, 40, 19, 25, 156, 86, 107, 248, 15, 234, 20, 136, 159, 90, 18, 116, 197, 91, 114, 206, 162, 11, 255, 171, 249, 29, 79, 105, 38, 152, 76, 255, 106, 69, 208, 108, 23, 246, 54 }, new byte[] { 82, 120, 200, 197, 116, 169, 188, 222, 249, 77, 194, 157, 195, 32, 16, 1, 144, 4, 133, 143, 163, 146, 209, 109, 40, 179, 94, 90, 202, 210, 126, 174, 64, 86, 87, 226, 85, 164, 196, 49, 138, 241, 32, 239, 153, 103, 126, 177, 176, 10, 254, 157, 193, 77, 234, 28, 184, 35, 231, 104, 193, 9, 183, 93, 15, 245, 41, 28, 78, 63, 51, 146, 200, 199, 44, 156, 252, 3, 31, 177, 155, 75, 105, 2, 57, 217, 77, 22, 168, 114, 237, 13, 123, 109, 161, 97, 159, 186, 142, 181, 210, 57, 223, 45, 184, 119, 81, 67, 163, 116, 252, 230, 18, 3, 199, 117, 145, 27, 19, 31, 52, 100, 169, 147, 233, 84, 119, 25 }, "", null },
                    { 2024, new DateTime(2022, 1, 17, 21, 5, 55, 266, DateTimeKind.Local).AddTicks(216), "", "themasteroftherain@protonmail.com", "Hoang", false, false, false, "Vo Xuan", new byte[] { 112, 216, 150, 109, 12, 16, 157, 53, 117, 217, 228, 43, 104, 49, 26, 24, 162, 88, 78, 160, 199, 211, 232, 105, 46, 35, 140, 141, 203, 234, 115, 212, 124, 255, 207, 61, 193, 100, 197, 218, 219, 165, 238, 149, 243, 23, 96, 111, 75, 118, 2, 141, 149, 31, 45, 91, 46, 6, 176, 250, 235, 60, 222, 135 }, new byte[] { 152, 115, 228, 38, 25, 219, 35, 220, 172, 154, 73, 241, 57, 202, 122, 225, 229, 192, 248, 152, 48, 174, 133, 31, 229, 31, 35, 60, 19, 111, 10, 68, 47, 167, 252, 117, 98, 99, 223, 110, 225, 247, 143, 95, 234, 112, 76, 246, 217, 137, 102, 160, 157, 99, 247, 164, 11, 48, 91, 209, 124, 112, 101, 219, 12, 137, 116, 84, 212, 105, 155, 81, 86, 248, 127, 248, 99, 90, 137, 206, 92, 159, 0, 30, 91, 28, 212, 80, 225, 122, 187, 141, 82, 232, 180, 33, 37, 175, 84, 113, 129, 80, 209, 248, 1, 179, 218, 104, 174, 159, 4, 6, 23, 152, 91, 60, 26, 224, 183, 116, 20, 233, 113, 58, 140, 176, 214, 212 }, "", null }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "ClassId", "Name", "Priority", "Weight" },
                values: new object[] { 2024, 2024, "Assignment 1 for the blind", 100, 4 });

            migrationBuilder.InsertData(
                table: "ClassStudentsAccounts",
                columns: new[] { "ClassId", "StudentAccountId", "Id" },
                values: new object[] { 2024, 2024, 2024 });

            migrationBuilder.InsertData(
                table: "ClassTeachersAccounts",
                columns: new[] { "ClassId", "TeacherId", "Id" },
                values: new object[] { 2024, 2023, 2024 });

            migrationBuilder.InsertData(
                table: "StudentsRecords",
                columns: new[] { "Id", "ClassId", "FullName", "StudentIdentification" },
                values: new object[] { 2024, 2024, "Xuan Hoang Vo", "5037" });

            migrationBuilder.InsertData(
                table: "StudentAssignmentGrades",
                columns: new[] { "Id", "AssignmentId", "IsFinalized", "Point", "StudentRecordId" },
                values: new object[] { 2024, 2024, false, 0, 2024 });

            migrationBuilder.InsertData(
                table: "AssignmentGradeReviewRequests",
                columns: new[] { "Id", "DateCreated", "Description", "RequestState", "RequestedNewPoint", "StudentAssignmentGradeId" },
                values: new object[] { 2024, new DateTime(2022, 1, 17, 21, 5, 55, 268, DateTimeKind.Local).AddTicks(2780), "I want to raise to point to 100", 2, 100, 2024 });

            migrationBuilder.InsertData(
                table: "GradeReviewReply",
                columns: new[] { "Id", "AssignmentGradeReviewRequestId", "Content", "DateTime", "ReplierId" },
                values: new object[] { 2024, 2024, "Ok I will take a look", new DateTime(2022, 1, 17, 21, 5, 55, 268, DateTimeKind.Local).AddTicks(5683), 2023 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 5024);

            migrationBuilder.DeleteData(
                table: "ClassStudentsAccounts",
                keyColumns: new[] { "ClassId", "StudentAccountId" },
                keyValues: new object[] { 2024, 2024 });

            migrationBuilder.DeleteData(
                table: "ClassTeachersAccounts",
                keyColumns: new[] { "ClassId", "TeacherId" },
                keyValues: new object[] { 2024, 2023 });

            migrationBuilder.DeleteData(
                table: "GradeReviewReply",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "AssignmentGradeReviewRequests",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "StudentAssignmentGrades",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "StudentsRecords",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2024);
        }
    }
}
