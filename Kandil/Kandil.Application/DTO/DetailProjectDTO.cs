using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public class DetailProjectDTO
    {
        public string Title { get; set; }
        public string AboutProject { get; set; }
        public string VideoUrl { get; set; }
        public string MainImage { get; set; }
        public string LocationImage { get; set; }
        public string PdfName { get; set; }
        public IEnumerable<AdvantageProject> AdvantageProjects { get; set; }
        public IEnumerable<LocationProject> LocationProjects { get; set; }
        public IEnumerable<ProjectImage> ProjectImages { get; set; }
    }
}
