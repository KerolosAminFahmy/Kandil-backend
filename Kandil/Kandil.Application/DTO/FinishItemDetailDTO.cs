using kandil.Domain.Entities;
using Kandil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class FinishItemDetailDTO
    {
        public FinishItem finishItem {  get; set; } 
        public IEnumerable<FinishImage> finishImages { get; set; }  
    }
}
