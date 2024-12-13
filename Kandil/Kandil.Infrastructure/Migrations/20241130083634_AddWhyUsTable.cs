using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWhyUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhyUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhyUs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WhyUs",
                columns: new[] { "Id", "Description", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 1, "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", "", "لماذا قنديل" },
                    { 2, "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", "image2.jpg", "الثقة" },
                    { 3, "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", "image3.jpg", "الإلتزام" },
                    { 4, "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", "image4.jpg", "الجودة" },
                    { 5, "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", "image4.jpg", "المصداقية" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhyUs");
        }
    }
}
