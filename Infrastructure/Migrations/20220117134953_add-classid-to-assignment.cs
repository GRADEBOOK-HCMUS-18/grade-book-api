using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addclassidtoassignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Assignments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AdminAccounts",
                columns: new[] { "Id", "DateCreated", "IsSuperAdmin", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 2022, new DateTime(2022, 1, 16, 14, 29, 12, 605, DateTimeKind.Local).AddTicks(4617), true, new byte[] { 86, 178, 153, 183, 168, 202, 242, 173, 10, 96, 89, 68, 154, 99, 121, 182, 243, 157, 106, 220, 228, 177, 122, 148, 195, 68, 107, 235, 19, 129, 114, 232, 108, 75, 94, 225, 167, 188, 230, 170, 123, 156, 171, 228, 54, 246, 118, 247, 217, 48, 177, 188, 240, 19, 157, 192, 76, 240, 159, 1, 102, 32, 212, 218 }, new byte[] { 231, 169, 99, 112, 169, 112, 22, 186, 184, 30, 79, 94, 90, 139, 32, 200, 20, 13, 71, 28, 49, 233, 248, 17, 15, 19, 232, 13, 71, 130, 12, 250, 170, 200, 201, 186, 238, 55, 14, 21, 77, 0, 240, 107, 191, 113, 33, 255, 93, 60, 122, 98, 121, 61, 120, 106, 232, 237, 236, 153, 253, 218, 242, 88, 143, 43, 116, 242, 227, 179, 126, 83, 106, 228, 189, 22, 45, 89, 14, 231, 107, 118, 36, 177, 58, 64, 255, 35, 48, 11, 118, 149, 34, 73, 198, 134, 158, 53, 101, 99, 152, 49, 217, 15, 178, 189, 57, 91, 73, 62, 29, 88, 177, 140, 32, 18, 105, 241, 48, 254, 40, 203, 43, 143, 93, 14, 158, 231 }, "admin" });
        }
    }
}
