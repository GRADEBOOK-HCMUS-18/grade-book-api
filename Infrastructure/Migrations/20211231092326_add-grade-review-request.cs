using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class addgradereviewrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentGradeReviewRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentAssignmentGradeId = table.Column<int>(type: "integer", nullable: false),
                    RequestedNewPoint = table.Column<int>(type: "integer", nullable: false),
                    RequestState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentGradeReviewRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentGradeReviewRequests_StudentAssignmentGrades_Stude~",
                        column: x => x.StudentAssignmentGradeId,
                        principalTable: "StudentAssignmentGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentGradeReviewRequests_StudentAssignmentGradeId",
                table: "AssignmentGradeReviewRequests",
                column: "StudentAssignmentGradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentGradeReviewRequests");
        }
    }
}
