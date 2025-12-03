using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMaintenanceData",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceData",
                table: "MaintenanceRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "MaintenanceRequests",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceDate",
                table: "MaintenanceRequests",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Hardwares",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivatedAt",
                table: "Hardwares",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "MaintenanceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletedMaintenanceDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    MaintenanceId = table.Column<int>(type: "integer", nullable: false),
                    MRequestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceHistory_MaintenanceRequests_MRequestId",
                        column: x => x.MRequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MRequestId",
                table: "MaintenanceHistory",
                column: "MRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceDate",
                table: "MaintenanceRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceData",
                table: "MaintenanceRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceData",
                table: "MaintenanceRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Hardwares",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivatedAt",
                table: "Hardwares",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
