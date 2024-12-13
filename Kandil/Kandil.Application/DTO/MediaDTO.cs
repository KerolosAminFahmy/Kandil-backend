using kandil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kandil.Application.DTO
{
    public class MediaDTO
    {
        public int? Id { get; set; } // Optional for create operations.

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be less than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        public DateTime Created { get; set; }

        public string? ImageName { get; set; }

        [Required(ErrorMessage = "Video URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string videoURl { get; set; }
        public int MediaId { get; set; }

        public IFormFile? Image { get; set; }

        public ICollection<IFormFile>? DetailImage { get; set; }

        public List<int>? AllRemovedImages { get; set; }
        public List<MediaImages>? UnitImages { get; set; }


    }
}
