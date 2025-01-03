using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class CoverImageDTO
    {
        public int? Id { get; set; }
        public string? NameImage { get; set; }
        public string MediaType { get; set; }

        public IFormFile? formFile { get; set; }
    }
}
