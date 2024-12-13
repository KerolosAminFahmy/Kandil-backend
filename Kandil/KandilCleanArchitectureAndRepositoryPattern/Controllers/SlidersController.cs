using kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> AddSliderItem([FromForm] IFormFile media, [FromForm] string mediaType)
        {
            if (media == null || media.Length == 0 || (mediaType != "image" && mediaType != "video"))
                return BadRequest("Invalid file or media type.");

            var FileName = await SaveImage(media);

            var sliderItem = new SliderItem
            {
                MediaType = mediaType,
                MediaPath = FileName
            };

            _context.SliderItems.Add(sliderItem);
            await _context.SaveChangesAsync();

            return Ok(sliderItem);
        }
        [HttpGet()]
        public async Task<IActionResult> GetSliderItems()
        {
            var sliderItems = await _context.SliderItems
                .ToListAsync();

            return Ok(sliderItems);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.SliderItems.SingleOrDefaultAsync(e=>e.Id==id);
            if(item == null)
                return NotFound();
            await RemoveImage(item.MediaPath);
            _context.SliderItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task<string> SaveImage(IFormFile Imgae)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(Imgae.FileName);

            var filePath = Path.Combine("Uploads", "Slider", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Slider", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }

    }
}
