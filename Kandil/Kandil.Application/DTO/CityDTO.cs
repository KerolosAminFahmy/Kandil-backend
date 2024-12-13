using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class CityDTO
    {
        [Required(ErrorMessage = "يرجي اداخال الاسم المدينه")]
        [MaxLength(50,ErrorMessage ="يرجي اداخال الاسم المدينه اقل من 50 حرف")]
        public string Title { get; set; }
        [Required()]
        public IFormFile Image { get; set; }


    }
}
