using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class EditAreaDTO
    {
        [Required(ErrorMessage = "الاسم مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب أن يكون الاسم بين {2} و {1} حرفًا.", MinimumLength = 3)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "الصورة مطلوبة.")]
        [DataType(DataType.Upload, ErrorMessage = "يجب أن تكون الصورة ملفًا صحيحًا.")]
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif,bmp", ErrorMessage = "يجب أن تكون الصورة بامتداد صحيح (jpg, jpeg, png, gif, bmp).")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "رقم المدينة مطلوب.")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب أن يكون رقم المدينة أكبر من 0.")]
        public int CityId { get; set; }
    }
}
