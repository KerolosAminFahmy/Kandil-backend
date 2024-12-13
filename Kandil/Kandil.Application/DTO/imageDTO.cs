using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class imageDTO
    {
        public IFormFile Image { get; set; }
        public string Text { get; set; }

    }
}
