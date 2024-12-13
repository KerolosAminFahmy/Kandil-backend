using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Domain.Entities
{
    public class MediaImages
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        
        [ForeignKey("Media")]
        public int MediaId { get; set; }
        public Media Media { get; set; }
    }
}
