using Kandil.Application.DTO;
using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class ViewUpdateDTO
    {
        public string Title { get; set; }
        public string AboutProject { get; set; }
        public string VideoURL { get; set; }
        public string MainImage { get; set; }
        public string LocationImage { get; set; }
        public string  PdfFile { get; set; }
        public ICollection<LocationProject> LocationProjects { get; set; }
        public ICollection<AdvantageProject> advantageProjects { get; set; }
        public ICollection<string> Images { get; set; }

    }
}
