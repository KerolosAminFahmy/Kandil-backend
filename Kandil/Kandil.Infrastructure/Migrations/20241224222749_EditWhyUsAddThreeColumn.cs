using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditWhyUsAddThreeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "WhyUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "WhyUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullDescription", "Quote" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FullDescription", "Quote" },
                values: new object[] { "", "في عالم يزداد فيه التنافس يومًا بعد يوم،" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FullDescription", "Quote" },
                values: new object[] { "", "شركة “قنديل للإستثمار العقاري” تلتزم بـ:" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FullDescription", "Quote" },
                values: new object[] { "", "تُؤكد شركة قنديل على التزامها باستمرار تطبيق فلسفة الجودة في جميع جوانب عملها..." });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FullDescription", "Quote" },
                values: new object[] { "", "نُؤكد على التزامنا بالمصداقية في جميع تعاملاتنا مع عملائنا، ونُؤمن بأنّها هي أساس بناء علاقات قوية ودائمة. فنحن نُقدّر ثقتكم بنا ونُحافظ عليها من خلال الالتزام بالمبادئ والقيم التي نؤمن بها." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "WhyUs");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "WhyUs");
        }
    }
}
