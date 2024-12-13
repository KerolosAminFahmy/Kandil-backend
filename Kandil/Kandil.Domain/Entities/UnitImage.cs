using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class UnitImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }

        public int UnitId { get; set; }
        public Units Unit { get; set; }
    }
}
