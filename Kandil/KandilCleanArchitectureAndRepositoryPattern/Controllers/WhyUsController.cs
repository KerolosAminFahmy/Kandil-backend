using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhyUsController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _context.WhyUs.ToList();
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var item = _context.WhyUs.SingleOrDefault(e=>e.Id==id);
            return Ok(item);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] WhyUs item)
        {
            var existingItem = await _context.WhyUs.FindAsync(id);
            if (existingItem == null) return NotFound();

            //existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.FullDescription = item.FullDescription;
            existingItem.Quote = item.Quote;
            if (item.ImageFile != null) { 
                bool isok = true; 
                if (existingItem.ImageUrl != null && !existingItem.ImageUrl.IsNullOrEmpty())
                {
                   isok = await RemoveImage(existingItem.ImageUrl);
                }
               existingItem.ImageUrl =  await SaveImage(item.ImageFile);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("UploadImage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile formFile)
        {
            var existingItem = await _context.WhyUs.FindAsync(1);
            if (existingItem == null) return NotFound();
            if (existingItem.ImageUrl != null || existingItem.ImageUrl!="") {
                await RemoveImage(existingItem.ImageUrl);
            }
           
            existingItem.ImageUrl = await SaveImage(formFile);
            await _context.SaveChangesAsync();
            return Ok();
            
        }
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "WhyUs", imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }
        private async Task<string> SaveImage(IFormFile Imgae)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(Imgae.FileName);

            var filePath = Path.Combine("Uploads", "WhyUs", imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Imgae.CopyToAsync(stream);
            }
            return imageName;

        }
        private async Task<bool> RemoveImage(string ImageName)
        {
            if (!string.IsNullOrEmpty(ImageName))
            {
                var oldImagePath = Path.Combine("Uploads", "WhyUs", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }

    }
}
