using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changeadmintabletostandalone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminAccounts_Users_UserId",
                table: "AdminAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AdminAccounts_UserId",
                table: "AdminAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdminAccounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AdminAccounts",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AdminAccounts",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AdminAccounts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AdminAccounts");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AdminAccounts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "AdminAccounts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdminAccounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccounts_UserId",
                table: "AdminAccounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminAccounts_Users_UserId",
                table: "AdminAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
