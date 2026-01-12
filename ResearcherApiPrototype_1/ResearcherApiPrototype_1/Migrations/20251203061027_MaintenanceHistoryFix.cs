using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceHistoryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceRequests_MRequestId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_MRequestId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "MRequestId",
                table: "MaintenanceHistory");

            migrationBuilder.RenameColumn(
                name: "MaintenanceId",
                table: "MaintenanceHistory",
                newName: "MaintenanceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MaintenanceRequestId",
                table: "MaintenanceHistory",
                column: "MaintenanceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceHistory",
                column: "MaintenanceRequestId",
                principalTable: "MaintenanceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_MaintenanceRequestId",
                table: "MaintenanceHistory");

            migrationBuilder.RenameColumn(
                name: "MaintenanceRequestId",
                table: "MaintenanceHistory",
                newName: "MaintenanceId");

            migrationBuilder.AddColumn<int>(
                name: "MRequestId",
                table: "MaintenanceHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MRequestId",
                table: "MaintenanceHistory",
                column: "MRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceRequests_MRequestId",
                table: "MaintenanceHistory",
                column: "MRequestId",
                principalTable: "MaintenanceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
