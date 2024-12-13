using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class FinishItemDTO
    {

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NameLocation { get; set; }
        public string VideoUrl { get; set; }
        public int FinishCategoryId { get; set; }
        public ICollection<IFormFile>? DetailImage { get; set; }

        public List<int>? AllRemovedImages { get; set; }



    }
}
