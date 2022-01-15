using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class datetimeadminaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AdminAccounts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 1, 15, 14, 28, 53, 96, DateTimeKind.Local).AddTicks(1743), new byte[] { 215, 205, 237, 201, 28, 18, 93, 8, 10, 16, 144, 242, 160, 219, 186, 70, 1, 46, 234, 143, 79, 160, 165, 29, 111, 34, 179, 109, 58, 20, 38, 110, 134, 138, 22, 254, 229, 155, 128, 47, 107, 192, 30, 88, 144, 64, 241, 159, 5, 54, 74, 148, 8, 219, 252, 84, 78, 236, 4, 214, 0, 159, 173, 204 }, new byte[] { 154, 53, 121, 42, 89, 155, 156, 17, 82, 178, 34, 233, 155, 244, 54, 223, 66, 253, 162, 242, 122, 99, 169, 137, 9, 198, 192, 5, 45, 16, 36, 200, 184, 45, 249, 23, 207, 24, 194, 179, 209, 229, 137, 97, 136, 138, 68, 179, 231, 237, 188, 27, 9, 135, 66, 150, 157, 36, 5, 150, 132, 169, 171, 121, 145, 150, 211, 179, 227, 140, 77, 7, 119, 181, 136, 210, 25, 237, 247, 217, 107, 175, 241, 129, 222, 187, 195, 233, 175, 61, 108, 129, 86, 97, 155, 11, 239, 174, 37, 253, 214, 15, 250, 71, 194, 198, 157, 232, 35, 204, 87, 134, 94, 254, 102, 161, 134, 167, 73, 211, 34, 6, 97, 192, 236, 12, 150, 239 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AdminAccounts");

            migrationBuilder.UpdateData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 53, 118, 170, 138, 130, 5, 13, 9, 140, 72, 7, 230, 18, 156, 52, 126, 11, 242, 124, 33, 194, 160, 37, 255, 35, 103, 242, 10, 119, 189, 154, 133, 124, 89, 131, 1, 17, 233, 43, 177, 45, 164, 150, 238, 70, 130, 173, 54, 122, 132, 166, 101, 204, 96, 69, 174, 89, 231, 143, 121, 137, 206, 48, 114 }, new byte[] { 211, 199, 31, 181, 155, 218, 12, 254, 97, 199, 177, 98, 178, 74, 204, 215, 231, 232, 33, 157, 90, 177, 155, 247, 227, 198, 55, 153, 37, 201, 3, 170, 6, 226, 118, 131, 60, 212, 35, 30, 22, 154, 105, 55, 25, 88, 160, 92, 150, 46, 217, 137, 208, 24, 227, 113, 206, 249, 171, 36, 219, 130, 203, 41, 157, 107, 127, 190, 169, 57, 244, 251, 141, 70, 151, 171, 65, 72, 41, 189, 209, 252, 218, 11, 226, 15, 135, 118, 242, 200, 44, 135, 213, 200, 151, 141, 103, 55, 139, 242, 22, 179, 242, 209, 104, 235, 71, 23, 233, 250, 167, 170, 86, 69, 28, 218, 81, 101, 186, 17, 197, 128, 70, 56, 175, 10, 46, 135 } });
        }
    }
}
