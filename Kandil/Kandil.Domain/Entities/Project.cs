using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AboutProject { get; set; }
        public string VideoUrl { get; set; }
        public string ImageName { get; set; }
        public string PdfName { get; set; }
        public string ImageNameLocation { get; set; }
        [ForeignKey("Area")]
        public int AreaId { get; set; }
        [JsonIgnore]
        public area Area { get; set; }
    }
}
