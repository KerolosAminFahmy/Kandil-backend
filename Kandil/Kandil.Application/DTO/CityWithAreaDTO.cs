using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class CityWithAreaDTO
    {
        public City city { get; set; }
        public IEnumerable<area> areas { get; set; }
    }
}
