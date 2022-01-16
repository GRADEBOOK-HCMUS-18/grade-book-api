using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class datetimeuseraccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 1, 16, 14, 29, 12, 605, DateTimeKind.Local).AddTicks(4617), new byte[] { 86, 178, 153, 183, 168, 202, 242, 173, 10, 96, 89, 68, 154, 99, 121, 182, 243, 157, 106, 220, 228, 177, 122, 148, 195, 68, 107, 235, 19, 129, 114, 232, 108, 75, 94, 225, 167, 188, 230, 170, 123, 156, 171, 228, 54, 246, 118, 247, 217, 48, 177, 188, 240, 19, 157, 192, 76, 240, 159, 1, 102, 32, 212, 218 }, new byte[] { 231, 169, 99, 112, 169, 112, 22, 186, 184, 30, 79, 94, 90, 139, 32, 200, 20, 13, 71, 28, 49, 233, 248, 17, 15, 19, 232, 13, 71, 130, 12, 250, 170, 200, 201, 186, 238, 55, 14, 21, 77, 0, 240, 107, 191, 113, 33, 255, 93, 60, 122, 98, 121, 61, 120, 106, 232, 237, 236, 153, 253, 218, 242, 88, 143, 43, 116, 242, 227, 179, 126, 83, 106, 228, 189, 22, 45, 89, 14, 231, 107, 118, 36, 177, 58, 64, 255, 35, 48, 11, 118, 149, 34, 73, 198, 134, 158, 53, 101, 99, 152, 49, 217, 15, 178, 189, 57, 91, 73, 62, 29, 88, 177, 140, 32, 18, 105, 241, 48, 254, 40, 203, 43, 143, 93, 14, 158, 231 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 1, 15, 14, 28, 53, 96, DateTimeKind.Local).AddTicks(1743), new byte[] { 215, 205, 237, 201, 28, 18, 93, 8, 10, 16, 144, 242, 160, 219, 186, 70, 1, 46, 234, 143, 79, 160, 165, 29, 111, 34, 179, 109, 58, 20, 38, 110, 134, 138, 22, 254, 229, 155, 128, 47, 107, 192, 30, 88, 144, 64, 241, 159, 5, 54, 74, 148, 8, 219, 252, 84, 78, 236, 4, 214, 0, 159, 173, 204 }, new byte[] { 154, 53, 121, 42, 89, 155, 156, 17, 82, 178, 34, 233, 155, 244, 54, 223, 66, 253, 162, 242, 122, 99, 169, 137, 9, 198, 192, 5, 45, 16, 36, 200, 184, 45, 249, 23, 207, 24, 194, 179, 209, 229, 137, 97, 136, 138, 68, 179, 231, 237, 188, 27, 9, 135, 66, 150, 157, 36, 5, 150, 132, 169, 171, 121, 145, 150, 211, 179, 227, 140, 77, 7, 119, 181, 136, 210, 25, 237, 247, 217, 107, 175, 241, 129, 222, 187, 195, 233, 175, 61, 108, 129, 86, 97, 155, 11, 239, 174, 37, 253, 214, 15, 250, 71, 194, 198, 157, 232, 35, 204, 87, 134, 94, 254, 102, 161, 134, 167, 73, 211, 34, 6, 97, 192, 236, 12, 150, 239 } });
        }
    }
}
