using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class ViewUnitUpdateDTO
    {
        public string Title { get; set; }


        public string Description { get; set; }

        public string TypePrice { get; set; }
        public string NameLocation { get; set; }

        public string Image { get; set; }

        public string Status { get; set; }

        public string CodeUnit { get; set; }

        public long Area { get; set; }
        public int NumberBathroom { get; set; }

        public int NumberRoom { get; set; }

        public int YearOfBuild { get; set; }

        public double Price { get; set; }

        public string VideoUrl { get; set; }

        public ICollection<string> DetailImage { get; set; }
        public IEnumerable<AdvantageUnit> Advantage { get; set; }
        public IEnumerable<ServiceUnit> Services { get; set; }
    }
}
