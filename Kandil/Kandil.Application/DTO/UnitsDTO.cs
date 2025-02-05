using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class UnitsDTO
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "العنوان مطلوب.")]
        [StringLength(150, ErrorMessage = "يجب ألا يزيد العنوان عن {1} حرفًا.")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "الوصف مطلوب.")]
        [StringLength(1000, ErrorMessage = "يجب ألا يزيد الوصف عن {1} حرفًا.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "الصورة مطلوبة.")]
        public IFormFile Image { get; set; }

        //[Required(ErrorMessage = "حالة الوحدة مطلوبة.")]
        [RegularExpression("^(Available|Sold)$", ErrorMessage = "الحالة يجب أن تكون 'Available', 'Sold',")]
        public string? Status { get; set; }

        //[Required(ErrorMessage = "كود الوحدة مطلوب.")]
        [StringLength(20, ErrorMessage = "يجب ألا يزيد كود الوحدة عن {1} حرفًا.")]
        public string? CodeUnit { get; set; }

        [Required(ErrorMessage = "المساحة مطلوبة.")]
        [Range(1, long.MaxValue, ErrorMessage = "يجب أن تكون المساحة أكبر من 0.")]
        public long Area { get; set; }

        [Required(ErrorMessage = "عدد الحمامات مطلوب.")]
        [Range(1, int.MaxValue, ErrorMessage = "عدد الحمامات يجب أن يكون أكبر من 0.")]
        public int NumberBathroom { get; set; }

        [Required(ErrorMessage = "عدد الغرف مطلوب.")]
        [Range(1, int.MaxValue, ErrorMessage = "عدد الغرف يجب أن يكون أكبر من 0.")]
        public int NumberRoom { get; set; }

        //[Required(ErrorMessage = "سنة البناء مطلوبة.")]
        [Range(1800, 2100, ErrorMessage = "سنة البناء يجب أن تكون بين 1800 و 2100.")]
        public int? YearOfBuild { get; set; }

        [Required(ErrorMessage = "السعر مطلوب.")]
        [Range(1, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من 0.")]
        public double Price { get; set; }

        //[Required(ErrorMessage = "رابط الفيديو مطلوب.")]
        [Url(ErrorMessage = "يجب أن يكون رابط الفيديو صالحًا.")]
        public string? VideoUrl { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NameLocation { get; set; }
        public string TypePrice { get; set; }
        public bool IsShown { get; set; }
        public ICollection<IFormFile>? DetailImage { get; set; }
        public IEnumerable<string>? Advantage { get; set; }
        public IEnumerable<string>? Services { get; set; }
    }
}
