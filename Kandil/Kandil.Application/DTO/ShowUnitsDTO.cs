using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class ShowUnitsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsShown { get; set; }
        public string NameLocation { get; set; }
        public string TypePrice { get; set; }
        public string CodeUnit { get; set; }
        public long Area { get; set; }
        public int NumberBathroom { get; set; }
        public int NumberRoom { get; set; }
        public int YearOfBuild { get; set; }
        public double Price { get; set; }
        public string VideoUrl { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public IEnumerable<AdvantageUnit> advantageUnits { get; set; }
        public IEnumerable<ServiceUnit> serviceUnits { get; set; }
        public IEnumerable<UnitImage> unitImages { get; set; }

    }
}
