using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class AdvantageUnit
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public Units Unit { get; set; }
    }
}
