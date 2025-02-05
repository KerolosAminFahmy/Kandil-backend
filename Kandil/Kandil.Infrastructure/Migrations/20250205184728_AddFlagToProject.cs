using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFlagToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinish",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinish",
                table: "Projects");
        }
    }
}
