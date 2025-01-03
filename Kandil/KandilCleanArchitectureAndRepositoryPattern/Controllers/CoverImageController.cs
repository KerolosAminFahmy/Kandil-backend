using kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverImageController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly UnitOfWork work = new(dbContext);
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var AllCover = dbContext.coverImages.ToList();
            return Ok(AllCover);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Cover = dbContext.coverImages.SingleOrDefault(e=>e.Id==id);
            return Ok(Cover);
        }
        [HttpPut()]
        public async Task<IActionResult> Update(int id, [FromForm] CoverImageDTO item)
        {
            var existingItem = await dbContext.coverImages.FindAsync(id);
            if (existingItem == null) return NotFound();

            existingItem.ImageType = item.MediaType;
            if (!existingItem.ImageName.IsNullOrEmpty()) {

                bool isOk = await RemoveImage(existingItem.ImageName);
                if (isOk && item.formFile != null) {

                    existingItem.ImageName = await SaveImage(item.formFile);
                
                }
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{imageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads","Cover", imageName);

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

            var filePath = Path.Combine("Uploads", "Cover", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Cover", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }
    }
}
