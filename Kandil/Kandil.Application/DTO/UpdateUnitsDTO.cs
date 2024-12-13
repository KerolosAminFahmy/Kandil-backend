using Kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class UpdateUnitsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public IFormFile? Image { get; set; } 
        public string Status { get; set; } 
        public string CodeUnit { get; set; } 
        public long Area { get; set; }
        public int NumberBathroom { get; set; }
        public int NumberRoom { get; set; }
        public int YearOfBuild { get; set; }
        public double Price { get; set; } 
        public string VideoUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NameLocation { get; set; }
        public string TypePrice { get; set; }

        public int ProjectId { get; set; }
        public List<AdvantageUnitUpdateDTO> AdvantageUnits { get; set; }
        public List<AdvantageUnitUpdateDTO> ServiceUnits { get; set; }
        public List<UnitImageDTO>? UnitImages { get; set; }
        public List<int>? AllRemovedAdvantage { get; set; }
        public List<int>? AllRemovedServices { get; set; } 
        public List<int>? AllRemovedImages { get; set; } 
    }
}
