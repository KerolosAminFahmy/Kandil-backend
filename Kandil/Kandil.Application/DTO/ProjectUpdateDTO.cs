using kandil.Application.DTO;
using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class ProjectUpdateDTO
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string AboutProject { get; set; }
        public string? VideoURL { get; set; }
        public bool IsFinish { get; set; }  
        public IFormFile? MainImage { get; set; }
        public IFormFile? LocationImage { get; set; }
        public IFormFile? pdfFile { get; set; }
        public ICollection<int>? LocationRemovedList { get; set; }
        public ICollection<int>? AdvantageRemovedList { get; set; }
        public ICollection<LocationProjectUpdateDTO>? LocationProjects { get; set; }
        public ICollection<AdvantageUpdateProjectDTO>? advantageUpdates { get; set; }

        public ICollection<string>? ImageRemoved { get; set; }
        public ICollection<imageDTO>? images { get; set; }

    }
}
