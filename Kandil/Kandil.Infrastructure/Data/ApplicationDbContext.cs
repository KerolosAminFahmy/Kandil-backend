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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WhyUs>().HasData(
                new WhyUs { Id = 1, Title = "لماذا قنديل", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "" },
                new WhyUs { Id = 2, Title = "الثقة", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "image2.jpg" },
                new WhyUs { Id = 3, Title = "الإلتزام", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "image3.jpg" },
                new WhyUs { Id = 4, Title = "الجودة", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "image4.jpg" },
                new WhyUs { Id = 5, Title = "المصداقية", Description = "\r\nالإنشاءات والعقارات والنمو الاقتصادي**: سأشرح كيف تسهم صناعة\r\nالإنشاءات والعقارات في زيادة الناتج المحلي الإجمالي للدولة", ImageUrl = "image4.jpg" }
            );
        }

    }
}
