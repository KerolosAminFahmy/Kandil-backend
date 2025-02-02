using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShownColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShown",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShown",
                table: "Units");
        }
    }
}
