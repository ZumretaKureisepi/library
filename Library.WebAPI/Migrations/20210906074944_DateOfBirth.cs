using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.WebAPI.Migrations
{
    public partial class DateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
