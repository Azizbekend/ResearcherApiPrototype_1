using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class schemeImageFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HardwareId",
                table: "SchemaImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HardwareId",
                table: "SchemaImages");
        }
    }
}
