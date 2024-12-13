using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class LocationProjectDTO
    {
        [Required(ErrorMessage ="يرجي اداخال الوقت المتوقع بالدقيقه")]
        public int Time { get; set; }
        [Required(ErrorMessage = "يرجي اداخال الاسم الشارع")]
        public string Street { get; set; }

    }
}
