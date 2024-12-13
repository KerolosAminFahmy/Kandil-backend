using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class AdvantageUpdateProjectDTO
    {
        public int id { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
    }
}
