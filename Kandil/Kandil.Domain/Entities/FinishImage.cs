using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Domain.Entities
{
    public class FinishImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("FinishItem")]
        public int FinishItemId { get; set; }
        public FinishItem FinishItem { get; set; }
    }
}
