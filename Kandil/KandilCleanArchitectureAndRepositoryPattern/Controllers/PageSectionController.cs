using kandil.Domain.Entities;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageSectionController(ApplicationDbContext dbContext) : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = dbContext.pageSections.ToList();
            return Ok(items);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] PageSection item)
        {
            var existingItem = await dbContext.pageSections.FindAsync(id);
            if (existingItem == null) return NotFound();

            existingItem.Text = item.Text;
            if (item.ImageFile != null)
            {
                bool isok = true;
                if (existingItem.ImageUrl != null)
                {
                    isok = await RemoveImage(existingItem.ImageUrl);
                }
                existingItem.ImageUrl = await SaveImage(item.ImageFile);
            }
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "PageSection", imageName);

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

            var filePath = Path.Combine("Uploads", "PageSection", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "PageSection", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }

    }
}
