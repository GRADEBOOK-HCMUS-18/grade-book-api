using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class adddefaultpicturehex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InviteString",
                table: "Classes",
                newName: "InviteStringTeacher");

            migrationBuilder.AddColumn<string>(
                name: "DefaultProfilePictureHex",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviteStringStudent",
                table: "Classes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultProfilePictureHex",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InviteStringStudent",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "InviteStringTeacher",
                table: "Classes",
                newName: "InviteString");
        }
    }
}
