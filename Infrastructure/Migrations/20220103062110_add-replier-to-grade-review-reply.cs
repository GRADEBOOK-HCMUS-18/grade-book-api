using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addrepliertogradereviewreply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplierId",
                table: "GradeReviewReply",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GradeReviewReply_ReplierId",
                table: "GradeReviewReply",
                column: "ReplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeReviewReply_Users_ReplierId",
                table: "GradeReviewReply",
                column: "ReplierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeReviewReply_Users_ReplierId",
                table: "GradeReviewReply");

            migrationBuilder.DropIndex(
                name: "IX_GradeReviewReply_ReplierId",
                table: "GradeReviewReply");

            migrationBuilder.DropColumn(
                name: "ReplierId",
                table: "GradeReviewReply");
        }
    }
}
