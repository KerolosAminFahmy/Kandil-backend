using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Domain.Entities
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
