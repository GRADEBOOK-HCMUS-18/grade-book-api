using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class addstudentassignmentgrade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentClassId_StudentIden~",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignmentGrades_StudentClassId_StudentIdentification",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropColumn(
                name: "StudentClassId",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropColumn(
                name: "StudentIdentification",
                table: "StudentAssignmentGrades");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIdentification",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AssignmentId",
                table: "StudentAssignmentGrades",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "StudentAssignmentGrades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentAssignmentGrades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentGrades_StudentId",
                table: "StudentAssignmentGrades",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentId",
                table: "StudentAssignmentGrades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentId",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignmentGrades_StudentId",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "StudentAssignmentGrades");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentAssignmentGrades");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIdentification",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignmentId",
                table: "StudentAssignmentGrades",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "StudentClassId",
                table: "StudentAssignmentGrades",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentIdentification",
                table: "StudentAssignmentGrades",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                columns: new[] { "ClassId", "StudentIdentification" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentGrades_StudentClassId_StudentIdentification",
                table: "StudentAssignmentGrades",
                columns: new[] { "StudentClassId", "StudentIdentification" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentGrades_Students_StudentClassId_StudentIden~",
                table: "StudentAssignmentGrades",
                columns: new[] { "StudentClassId", "StudentIdentification" },
                principalTable: "Students",
                principalColumns: new[] { "ClassId", "StudentIdentification" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
