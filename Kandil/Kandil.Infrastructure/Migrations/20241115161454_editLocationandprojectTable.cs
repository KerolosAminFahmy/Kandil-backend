using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editLocationandprojectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "LocationProjects",
                newName: "NameOfStreet");

            migrationBuilder.AddColumn<string>(
                name: "ImageNameLocation",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageNameLocation",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "NameOfStreet",
                table: "LocationProjects",
                newName: "Image");
        }
    }
}
