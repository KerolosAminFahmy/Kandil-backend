using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageName { get; set; }
        public string VideoURl { get; set; }

        [ForeignKey("MediaCategory")]
        public int MediaId { get; set; }
        public MediaCategory MediaCategory { get; set; }
    }
}
