using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class StaticObjFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "StaticObjectInfos",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "StaticObjectInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "StaticObjectInfos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "StaticObjectInfos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "StaticObjectInfos");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "StaticObjectInfos");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "StaticObjectInfos");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StaticObjectInfos",
                newName: "PhotoName");
        }
    }
}
