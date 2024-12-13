using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class AdvantageProjectDTO
    {
        [Required(ErrorMessage ="يرجي اداخال ميزه مشروع")]
        [MaxLength(15,ErrorMessage = "يرجي اداخال الاسم الميزه اقل من 15 حرف")]
        public string Text { get; set; }
        [Required(ErrorMessage ="يرجي ارفاق صوره للتوضيح الميزه")]
        public IFormFile Image { get; set; }
    }
}
