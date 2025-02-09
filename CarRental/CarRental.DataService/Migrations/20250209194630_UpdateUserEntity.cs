using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                schema: "security",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Gendor",
                schema: "security",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gendor",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Modified",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "security",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                schema: "security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
