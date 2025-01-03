using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverImage",
                table: "CoverImage");

            migrationBuilder.RenameTable(
                name: "CoverImage",
                newName: "coverImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coverImages",
                table: "coverImages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_coverImages",
                table: "coverImages");

            migrationBuilder.RenameTable(
                name: "coverImages",
                newName: "CoverImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverImage",
                table: "CoverImage",
                column: "Id");
        }
    }
}
