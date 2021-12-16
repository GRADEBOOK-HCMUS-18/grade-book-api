using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changestudentidtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentId",
                table: "StudentAssignmentGrades");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentAssignmentGrades",
                newName: "StudentRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentGrades_StudentId",
                table: "StudentAssignmentGrades",
                newName: "IX_StudentAssignmentGrades_StudentRecordId");

            migrationBuilder.RenameColumn(
                name: "Point",
                table: "Assignments",
                newName: "Weight");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentRecordId",
                table: "StudentAssignmentGrades",
                column: "StudentRecordId",
                principalTable: "Students",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentRecordId",
                table: "StudentAssignmentGrades");

            migrationBuilder.RenameColumn(
                name: "StudentRecordId",
                table: "StudentAssignmentGrades",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentGrades_StudentRecordId",
                table: "StudentAssignmentGrades",
                newName: "IX_StudentAssignmentGrades_StudentId");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Assignments",
                newName: "Point");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentId",
                table: "StudentAssignmentGrades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
