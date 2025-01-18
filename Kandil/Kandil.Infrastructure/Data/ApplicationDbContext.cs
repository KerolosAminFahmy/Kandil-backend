using kandil.Domain.Entities;
using Kandil.Domain.Entities;
using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<City> Cities {  get; set; }
        public DbSet<area> Areas {  get; set; }
        public DbSet<Project> Projects {  get; set; }
        public DbSet<AdvantageProject> AdvantageProjects {  get; set; }
        public DbSet<LocationProject> LocationProjects {  get; set; }
        public DbSet<ProjectImage> projectImages {  get; set; }

        public DbSet<Units> Units { get; set; }
        public DbSet<AdvantageUnit> AdvantageUnits { get; set; }
        public DbSet<UnitImage> UnitImages { get; set; }
        public DbSet<ServiceUnit> ServiceUnits { get; set; }

        public DbSet<MediaCategory> mediaCategories { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<MediaImages> MediaImages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<WhyUs> WhyUs { get; set; }
        public DbSet<SliderItem> SliderItems { get; set; }
        public DbSet<FinishCategory> finishCategories { get; set; }
        public DbSet<FinishItem> finishItems { get; set; }
        public DbSet<FinishImage> finishImages { get; set; }

        public DbSet<CoverImage> coverImages { get; set; }
        public DbSet<PageSection> pageSections { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WhyUs>().HasData(
                new WhyUs { Id = 1, Title = "لماذا قنديل", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "" ,FullDescription="", Quote = "" },
                new WhyUs { Id = 2, Title = "الثقة", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "e8005130-7693-4faf-a4e1-68284c1f6c59.png", FullDescription= "<div>\r\n<div>تُدرك شركة قنديل أهمية بناء علاقات قوية وناجحة مع عملائها...</div>\r\n</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>\r\n<div style=\"position: absolute; left: -65535px;\">&nbsp;</div>", Quote = "في عالم يزداد فيه التنافس يومًا بعد يوم،" },
                new WhyUs { Id = 3, Title = "الإلتزام", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "66d68132-52f9-42d6-a424-55e59ea7b1c3.png", FullDescription= "<ul class=\"ng-star-inserted\">\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الوفاء بالوعود</strong>: تنفيذ جميع الوعود التي تم تقديمها للعميل دون أي تهاون.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالمواعيد</strong>: تسليم الوحدات العقارية في الوقت المحدد دون تأخير.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالجودة</strong>: تنفيذ جميع الأعمال وفقًا للمواصفات والمعايير المتفق عليها.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالشفافية</strong>: تقديم جميع المعلومات والبيانات المتعلقة بالعقارات للعميل بشكل واضح وصريح.</li>\r\n<li class=\"ng-star-inserted\" style=\"text-align: center;\"><strong>الالتزام بالدعم</strong>: تقديم خدمات ما بعد البيع للعملاء وتلبية احتياجاتهم.</li>\r\n</ul>", Quote = "شركة “قنديل للإستثمار العقاري” تلتزم بـ:" },
                new WhyUs { Id = 4, Title = "الجودة", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "18f6a9bb-fd51-49b6-9e50-76362bca1d24.png", FullDescription= "<div>\r\n<div>تحرص شركة قنديل على الاستماع إلى آراء وملاحظات العملاء بشكل دائم...</div>\r\n</div>", Quote = "تُؤكد شركة قنديل على التزامها باستمرار تطبيق فلسفة الجودة في جميع جوانب عملها..." },
                new WhyUs { Id = 5, Title = "المصداقية", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "1327ea48-089e-4a94-b705-5cfd43bc19ff.png", FullDescription= "<section class=\"InfoContent\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-lg-12\">\r\n<div class=\"page-details-inner service-details-inner\">\r\n<p>ي شركة قنديل، نؤمن إيمانًا راسخًا بأهمية المصداقية في جميع تعاملاتنا مع عملائنا. فمنذ تأسيسنا، حرصنا على بناء علاقات قوية قائمة على الثقة والاحترام المتبادل. وإيمانًا منا بأهمية المصداقية، نودّ أن نُؤكد على التزامنا بالمبادئ التالية:</p>\r\n<ul class=\"ng-star-inserted\">\r\n<li class=\"ng-star-inserted\"><strong>الشفافية</strong>: نُؤمن بأنّ الشفافية هي أساس المصداقية.</li>\r\n<li class=\"ng-star-inserted\"><strong>الوفاء بالوعود</strong>: نُدرك أهمية الوفاء بالوعود التي نُقدمها لعملائنا.</li>\r\n<li class=\"ng-star-inserted\"><strong>الصدق والأمانة</strong>: نُؤمن بأنّ الصدق والأمانة هما أساس أيّ علاقة ناجحة.</li>\r\n<li class=\"ng-star-inserted\"><strong>احترام احتياجات العملاء</strong>: نُدرك أهمية احتياجات عملائنا ونُقدرها.</li>\r\n<li class=\"ng-star-inserted\"><strong>خدمة عملاء ممتازة</strong>: نُؤمن بأنّ خدمة العملاء هي العنصر الأساسي لضمان رضاهم.</li>\r\n</ul>\r\n</div>\r\n</div>\r\n</div>\r\n</div>\r\n</section>\r\n<div id=\"widgetWrapper\" class=\"bottom-right\">\r\n<div id=\"contentWrapper\" class=\"hide\"></div>\r\n<div id=\"widgetBubbleRow\"></div>\r\n</div>\r\n<section class=\"DiscoverArea\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-lg-12\">&nbsp;</div>\r\n</div>\r\n</div>\r\n</section>\r\n<footer>\r\n<div class=\"footer-top-area section-bg-2 plr--5\">\r\n<div class=\"container\">\r\n<div class=\"row\">\r\n<div class=\"col-xl-3 col-md-6 col-sm-6 col-12\">\r\n<div class=\"footer-widget footer-about-widget\">&nbsp;</div>\r\n</div>\r\n</div>\r\n</div>\r\n</div>\r\n</footer>", Quote= "نُؤكد على التزامنا بالمصداقية في جميع تعاملاتنا مع عملائنا، ونُؤمن بأنّها هي أساس بناء علاقات قوية ودائمة. فنحن نُقدّر ثقتكم بنا ونُحافظ عليها من خلال الالتزام بالمبادئ والقيم التي نؤمن بها." }
            );
            modelBuilder.Entity<CoverImage>().HasData(

                new CoverImage { Id = 1 , ImageName="14.jpg" , PageName = "مشروعات",ImageType="img"  },
                new CoverImage { Id = 2 , ImageName="14.jpg" , PageName = "لماذا قنديل", ImageType = "img" },
                new CoverImage { Id = 3 , ImageName="14.jpg", PageName = "تشطبيات", ImageType = "img" },
                new CoverImage { Id = 4 , ImageName="14.jpg", PageName = "مركز الاعلامي", ImageType = "img" },
                new CoverImage { Id = 5 , ImageName="14.jpg", PageName = "اتصل بنا", ImageType = "img" }


            );
            modelBuilder.Entity<PageSection>().HasData(

                new PageSection { Id = 1, Text = "", ImageUrl = "KAndil-lemaza-2.jpg" },
                new PageSection { Id = 2, Text = "", ImageUrl = "3-copy.png" },
                new PageSection { Id = 3, Text = "", ImageUrl = ""},
                new PageSection { Id = 4, Text = "", ImageUrl = ""},
                new PageSection { Id = 5, Text = "<p style=\"margin-bottom: 7rem;\">نحن نهتم بجودة مشاريعنا بتقديم أفضل بناء باستخدام أفضل خامات البناء بأعلى مستوى من التشطيب للواجهات الخارجة المصممة بإحترافية عالية على الإسلوبين الكلاسيك والمودرن .. بالاضافة الى خبراتنا التسويقية المتميزة التي تضمن لك أفضل استثمار بأعلى ربحية من خلال أفضل الطرق الاستثمارية الشاملة التي نقدمها لك. .</p>", ImageUrl = "فلسفتنا.png" },
                new PageSection { Id = 6, Text = "<p>نقدم شقق سكنية بالتجمع الخامس بأعلى مواصفات البناء والأكثر راحة للعملاء:</p><p style=\"margin-bottom: 7rem;\">(واجهات ألترا مودرن– بوابات الكترونية – كاميرات مراقبة – مداخل راقية جداً –&nbsp; أسانسير كامل – نافورة – مساحات خضراء – شلالات مياه – أمن وحراسة – جراج خاص لكل وحدة – تجهيز للدش المركزي والانتركم)</p>", ImageUrl = "خدماتنا.png" },
                new PageSection { Id = 7, Text = "<p>تهدف الشركة الى تحقيق أعلى درجات النجاح في السوق العقاري في مصر معتمدة في ذلك على الالتزام والتنفيذ والحصول على ثقة العملاء الذين لم نصل الى هذا القدر من النجاح الا بثقتهم الغالية في شركتنا وفي أعمالنا .. ونحن نعتز بذلك …وأيضاً طموحاتنا في التوسع والانتشار على أكبر رقعة أرضية في مصر وخاصة في المدن الجديدة … ساعيين في خدمة المجتمع للصالح العام .</p>", ImageUrl = "رؤيتنا.png" }


            );
        }

    }
}
