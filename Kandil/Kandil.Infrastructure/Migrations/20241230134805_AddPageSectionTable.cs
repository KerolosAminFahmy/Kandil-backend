using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPageSectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pageSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageSections", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "<div>\r\n<div>تُدرك شركة قنديل أهمية بناء علاقات قوية وناجحة مع عملائها...</div>\r\n</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>", "e8005130-7693-4faf-a4e1-68284c1f6c59.png" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "<ul class=\"ng-star-inserted\">\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الوفاء بالوعود</strong>: تنفيذ جميع الوعود التي تم تقديمها للعميل دون أي تهاون.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالمواعيد</strong>: تسليم الوحدات العقارية في الوقت المحدد دون تأخير.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالجودة</strong>: تنفيذ جميع الأعمال وفقًا للمواصفات والمعايير المتفق عليها.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالشفافية</strong>: تقديم جميع المعلومات والبيانات المتعلقة بالعقارات للعميل بشكل واضح وصريح.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالدعم</strong>: تقديم خدمات ما بعد البيع للعملاء وتلبية احتياجاتهم.</li>\r\n</ul>", "66d68132-52f9-42d6-a424-55e59ea7b1c3.png" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "<div>\r\n<div>تحرص شركة قنديل على الاستماع إلى آراء وملاحظات العملاء بشكل دائم...</div>\r\n</div>", "18f6a9bb-fd51-49b6-9e50-76362bca1d24.png" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "<section class=\"InfoContent\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-lg-12\">\r\n<div class=\"page-details-inner service-details-inner\">\r\n<p>ي شركة قنديل، نؤمن إيمانًا راسخًا بأهمية المصداقية في جميع تعاملاتنا مع عملائنا. فمنذ تأسيسنا، حرصنا على بناء علاقات قوية قائمة على الثقة والاحترام المتبادل. وإيمانًا منا بأهمية المصداقية، نودّ أن نُؤكد على التزامنا بالمبادئ التالية:</p>\r\n<ul class=\"ng-star-inserted\">\r\n<li class=\"ng-star-inserted\"><strong>الشفافية</strong>: نُؤمن بأنّ الشفافية هي أساس المصداقية.</li>\r\n<li class=\"ng-star-inserted\"><strong>الوفاء بالوعود</strong>: نُدرك أهمية الوفاء بالوعود التي نُقدمها لعملائنا.</li>\r\n<li class=\"ng-star-inserted\"><strong>الصدق والأمانة</strong>: نُؤمن بأنّ الصدق والأمانة هما أساس أيّ علاقة ناجحة.</li>\r\n<li class=\"ng-star-inserted\"><strong>احترام احتياجات العملاء</strong>: نُدرك أهمية احتياجات عملائنا ونُقدرها.</li>\r\n<li class=\"ng-star-inserted\"><strong>خدمة عملاء ممتازة</strong>: نُؤمن بأنّ خدمة العملاء هي العنصر الأساسي لضمان رضاهم.</li>\r\n</ul>\r\n</div>\r\n</div>\r\n</div>\r\n</div>\r\n</section>\r\n<div id=\"widgetWrapper\" class=\"bottom-right\">\r\n<div id=\"contentWrapper\" class=\"hide\"></div>\r\n<div id=\"widgetBubbleRow\"></div>\r\n</div>\r\n<section class=\"DiscoverArea\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-lg-12\">&nbsp;</div>\r\n</div>\r\n</div>\r\n</section>\r\n<footer>\r\n<div class=\"footer-top-area section-bg-2 plr--5\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-xl-3 col-md-6 col-sm-6 col-12\">\r\n<div class=\"footer-widget footer-about-widget\">&nbsp;</div>\r\n</div>\r\n</div>\r\n</div>\r\n</div>\r\n</footer>", "1327ea48-089e-4a94-b705-5cfd43bc19ff.png" });

            migrationBuilder.InsertData(
                table: "pageSections",
                columns: new[] { "Id", "ImageUrl", "Text" },
                values: new object[,]
                {
                    { 1, "KAndil-lemaza-2.jpg", "" },
                    { 2, "3-copy.png", "" },
                    { 3, "", "" },
                    { 4, "", "" },
                    { 5, "فلسفتنا.png", "<p style=\"margin-bottom: 7rem;\">نحن نهتم بجودة مشاريعنا بتقديم أفضل بناء باستخدام أفضل خامات البناء بأعلى مستوى من التشطيب للواجهات الخارجة المصممة بإحترافية عالية على الإسلوبين الكلاسيك والمودرن .. بالاضافة الى خبراتنا التسويقية المتميزة التي تضمن لك أفضل استثمار بأعلى ربحية من خلال أفضل الطرق الاستثمارية الشاملة التي نقدمها لك. .</p>" },
                    { 6, "خدماتنا.png", "<p>نقدم شقق سكنية بالتجمع الخامس بأعلى مواصفات البناء والأكثر راحة للعملاء:</p><p style=\"margin-bottom: 7rem;\">(واجهات ألترا مودرن– بوابات الكترونية – كاميرات مراقبة – مداخل راقية جداً –&nbsp; أسانسير كامل – نافورة – مساحات خضراء – شلالات مياه – أمن وحراسة – جراج خاص لكل وحدة – تجهيز للدش المركزي والانتركم)</p>" },
                    { 7, "رؤيتنا.png", "<p>تهدف الشركة الى تحقيق أعلى درجات النجاح في السوق العقاري في مصر معتمدة في ذلك على الالتزام والتنفيذ والحصول على ثقة العملاء الذين لم نصل الى هذا القدر من النجاح الا بثقتهم الغالية في شركتنا وفي أعمالنا .. ونحن نعتز بذلك …وأيضاً طموحاتنا في التوسع والانتشار على أكبر رقعة أرضية في مصر وخاصة في المدن الجديدة … ساعيين في خدمة المجتمع للصالح العام .</p>" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pageSections");

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "", "image2.jpg" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "", "image3.jpg" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "", "image4.jpg" });

            migrationBuilder.UpdateData(
                table: "WhyUs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FullDescription", "ImageUrl" },
                values: new object[] { "", "image4.jpg" });
        }
    }
}
