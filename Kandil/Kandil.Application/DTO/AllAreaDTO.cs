using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class AllAreaDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public List<ViewAreaDTO> ViewAreas { get; set; }

    }
}
