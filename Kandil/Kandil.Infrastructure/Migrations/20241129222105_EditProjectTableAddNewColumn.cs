using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditProjectTableAddNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfName",
                table: "Projects");
        }
    }
}
