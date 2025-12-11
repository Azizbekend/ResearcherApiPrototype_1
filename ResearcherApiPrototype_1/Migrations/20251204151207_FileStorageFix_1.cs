using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearcherApiPrototype_1.Migrations
{
    /// <inheritdoc />
    public partial class FileStorageFix_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Schemas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "SchemaImages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Hardwares",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileModel",
                table: "Hardwares",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schemas_FileId",
                table: "Schemas",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaImages_FileId",
                table: "SchemaImages",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchemaImages_Files_FileId",
                table: "SchemaImages",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schemas_Files_FileId",
                table: "Schemas",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchemaImages_Files_FileId",
                table: "SchemaImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemas_Files_FileId",
                table: "Schemas");

            migrationBuilder.DropIndex(
                name: "IX_Schemas_FileId",
                table: "Schemas");

            migrationBuilder.DropIndex(
                name: "IX_SchemaImages_FileId",
                table: "SchemaImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Schemas");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "SchemaImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Hardwares");

            migrationBuilder.DropColumn(
                name: "FileModel",
                table: "Hardwares");
        }
    }
}
