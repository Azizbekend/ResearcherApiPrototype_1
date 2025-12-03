using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceHistoryFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "MaintenanceRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "SheduleMaintenanceDate",
                table: "MaintenanceHistory",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheduleMaintenanceDate",
                table: "MaintenanceHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "MaintenanceRequests",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
