using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYHProjekt2API.Migrations
{
    public partial class UpdatedRequirements4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfMinutes",
                table: "TimeRegistrations");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TimeRegistrations",
                newName: "EventStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEnd",
                table: "TimeRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEnd",
                table: "TimeRegistrations");

            migrationBuilder.RenameColumn(
                name: "EventStart",
                table: "TimeRegistrations",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfMinutes",
                table: "TimeRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
