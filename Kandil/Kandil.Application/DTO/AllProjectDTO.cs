using kandil.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Application.DTO
{
    public  class AllProjectDTO
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public List<ViewProjectDTO> ViewProject { get; set; }
    }
}
