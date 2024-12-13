using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Domain.Entities
{
    public class FinishItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string NameLocation { get; set; }
        public string VideoUrl { get; set; }
        [ForeignKey("FinishCategory")]
        public int FinishCategoryId { get; set; }
        public FinishCategory FinishCategory { get; set; }

    }
}
