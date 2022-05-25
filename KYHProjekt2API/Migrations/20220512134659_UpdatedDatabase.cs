using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYHProjekt2API.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_Customers_CustomerId",
                table: "TimeRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_Customers_CustomerId",
                table: "TimeRegistrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_Customers_CustomerId",
                table: "TimeRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_Customers_CustomerId",
                table: "TimeRegistrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
