using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string ImageName { get; set; }
        [JsonIgnore]
        public ICollection<area> Areas { get; set; } = new List<area>();

    }
}
