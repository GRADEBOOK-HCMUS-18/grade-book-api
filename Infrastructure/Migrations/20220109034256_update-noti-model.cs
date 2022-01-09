using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class updatenotimodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "UserNotifications");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentGradeReviewRequestId",
                table: "UserNotifications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "UserNotifications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "UserNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "UserNotifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                table: "UserNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_AssignmentGradeReviewRequestId",
                table: "UserNotifications",
                column: "AssignmentGradeReviewRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_AssignmentId",
                table: "UserNotifications",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ClassId",
                table: "UserNotifications",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_AssignmentGradeReviewRequests_AssignmentG~",
                table: "UserNotifications",
                column: "AssignmentGradeReviewRequestId",
                principalTable: "AssignmentGradeReviewRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Assignments_AssignmentId",
                table: "UserNotifications",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Classes_ClassId",
                table: "UserNotifications",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_AssignmentGradeReviewRequests_AssignmentG~",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Assignments_AssignmentId",
                table: "UserNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Classes_ClassId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_AssignmentGradeReviewRequestId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_AssignmentId",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_ClassId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "AssignmentGradeReviewRequestId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "UserNotifications");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "UserNotifications",
                type: "text",
                nullable: true);
        }
    }
}
