using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class addstudentassignmentgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAssignmentGrades",
                columns: table => new
                {
                    StudentAssignmentGradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentClassId = table.Column<int>(type: "integer", nullable: true),
                    StudentIdentification = table.Column<string>(type: "text", nullable: true),
                    AssignmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignmentGrades", x => x.StudentAssignmentGradeId);
                    table.ForeignKey(
                        name: "FK_StudentAssignmentGrades_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignmentGrades_Students_StudentClassId_StudentIden~",
                        columns: x => new { x.StudentClassId, x.StudentIdentification },
                        principalTable: "Students",
                        principalColumns: new[] { "ClassId", "StudentIdentification" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentGrades_AssignmentId",
                table: "StudentAssignmentGrades",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentGrades_StudentClassId_StudentIdentification",
                table: "StudentAssignmentGrades",
                columns: new[] { "StudentClassId", "StudentIdentification" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssignmentGrades");
        }
    }
}
