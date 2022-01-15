using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class seedadmindata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdminAccounts",
                columns: new[] { "Id", "IsSuperAdmin", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 2022, true, new byte[] { 53, 118, 170, 138, 130, 5, 13, 9, 140, 72, 7, 230, 18, 156, 52, 126, 11, 242, 124, 33, 194, 160, 37, 255, 35, 103, 242, 10, 119, 189, 154, 133, 124, 89, 131, 1, 17, 233, 43, 177, 45, 164, 150, 238, 70, 130, 173, 54, 122, 132, 166, 101, 204, 96, 69, 174, 89, 231, 143, 121, 137, 206, 48, 114 }, new byte[] { 211, 199, 31, 181, 155, 218, 12, 254, 97, 199, 177, 98, 178, 74, 204, 215, 231, 232, 33, 157, 90, 177, 155, 247, 227, 198, 55, 153, 37, 201, 3, 170, 6, 226, 118, 131, 60, 212, 35, 30, 22, 154, 105, 55, 25, 88, 160, 92, 150, 46, 217, 137, 208, 24, 227, 113, 206, 249, 171, 36, 219, 130, 203, 41, 157, 107, 127, 190, 169, 57, 244, 251, 141, 70, 151, 171, 65, 72, 41, 189, 209, 252, 218, 11, 226, 15, 135, 118, 242, 200, 44, 135, 213, 200, 151, 141, 103, 55, 139, 242, 22, 179, 242, 209, 104, 235, 71, 23, 233, 250, 167, 170, 86, 69, 28, 218, 81, 101, 186, 17, 197, 128, 70, 56, 175, 10, 46, 135 }, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminAccounts",
                keyColumn: "Id",
                keyValue: 2022);
        }
    }
}
