using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class area
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City? City { get; set; }
        public ICollection<Project>? projects { get; set; }
    }
}
