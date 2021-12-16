using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class renamestudenttostudentRecord2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentRecordId",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentsRecords");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassId",
                table: "StudentsRecords",
                newName: "IX_StudentsRecords_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsRecords",
                table: "StudentsRecords",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_StudentsRecords_StudentRecordId",
                table: "StudentAssignmentGrades",
                column: "StudentRecordId",
                principalTable: "StudentsRecords",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsRecords_Classes_ClassId",
                table: "StudentsRecords",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_StudentsRecords_StudentRecordId",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsRecords_Classes_ClassId",
                table: "StudentsRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsRecords",
                table: "StudentsRecords");

            migrationBuilder.RenameTable(
                name: "StudentsRecords",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsRecords_ClassId",
                table: "Students",
                newName: "IX_Students_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentRecordId",
                table: "StudentAssignmentGrades",
                column: "StudentRecordId",
                principalTable: "Students",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
