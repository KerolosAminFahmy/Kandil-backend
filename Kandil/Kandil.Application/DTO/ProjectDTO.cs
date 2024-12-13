using kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Application.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class ProjectDTO
    {
        [Required(ErrorMessage ="يرجي اداخال اسم مشروع")]
        public string Title { get; set; }
        [Required(ErrorMessage = "يرجي اداخال تفاصيل مشروع")]
        public string AboutProject { get; set; }
        [Required(ErrorMessage = "يرجي اداخال لينك فيديو من يوتيوب")]
        public string VideoUrl { get; set; }

        public ICollection<imageDTO> Images { get; set; }

        public ICollection<AdvantageProjectDTO> advantageProjects { get; set; }
        public ICollection<LocationProjectDTO> locationProjects { get; set; }
        [Required(ErrorMessage ="يرجي رفع صوره الاساسيه للمشروع")]

        public IFormFile MainImage { get; set; }
        public IFormFile LocationImage { get; set; }
        public IFormFile pdfFile { get; set; }



        public int AreaId { get; set; }

    }
}
