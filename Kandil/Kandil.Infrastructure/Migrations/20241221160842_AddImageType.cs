using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverImage", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CoverImage",
                columns: new[] { "Id", "ImageName", "ImageType", "PageName" },
                values: new object[,]
                {
                    { 1, "14.jpg", "img", "مشروعات" },
                    { 2, "14.jpg", "img", "لماذا قنديل" },
                    { 3, "14.jpg", "img", "تشطبيات" },
                    { 4, "14.jpg", "img", "مركز الاعلامي" },
                    { 5, "14.jpg", "img", "اتصل بنا" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverImage");
        }
    }
}
