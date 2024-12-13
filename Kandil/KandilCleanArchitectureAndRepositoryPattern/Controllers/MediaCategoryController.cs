using kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kadnil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaCategoryController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        /// <summary>
        /// Creates a new media category.
        /// </summary>
        /// <param name="dto">The media category data transfer object containing title and image name.</param>
        /// <returns>The newly created media category.</returns>
        /// <response code="201">Returns the newly created media category.</response>
        /// <response code="400">If the input is invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] MediaCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = new MediaCategory
            {
                ImageName = await SaveImage(dto.Image),
                Title = dto.Title,
            };
            await unitOfWork.MediaCategory.AddAsync(created);
            unitOfWork.Complete();  
            return Ok(created);
        }

        /// <summary>
        /// Gets a media category by its ID.
        /// </summary>
        /// <param name="id">The ID of the media category to retrieve.</param>
        /// <returns>The media category with the specified ID.</returns>
        /// <response code="200">Returns the media category.</response>
        /// <response code="404">If the media category is not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await unitOfWork.MediaCategory.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }
        [HttpGet("GetName/{id:int}")]
        public async Task<IActionResult> GetNameById(int id)
        {
            var category = await unitOfWork.MediaCategory.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            string name = category.Title;
            return Ok(new { message = name });
        }
        /// <summary>
        /// Gets all media categories.
        /// </summary>
        /// <returns>A list of all media categories.</returns>
        /// <response code="200">Returns the list of media categories.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await unitOfWork.MediaCategory.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Updates a media category by its ID.
        /// </summary>
        /// <param name="id">The ID of the media category to update.</param>
        /// <param name="dto">The updated media category data transfer object.</param>
        /// <returns>The updated media category.</returns>
        /// <response code="200">Returns the updated media category.</response>
        /// <response code="400">If the input is invalid.</response>
        /// <response code="404">If the media category is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] MediaCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ExistMediaCategory= await unitOfWork.MediaCategory.GetByIdAsync(id);
                if (ExistMediaCategory != null)
                {
                    ExistMediaCategory.Title = dto.Title;
                    if (dto.Image != null)
                    {
                        var isOk = await RemoveImage(ExistMediaCategory.ImageName);
                        if (isOk)
                        {

                            var nameOfImage = await SaveImage(dto.Image);
                            ExistMediaCategory.ImageName = nameOfImage;
                        }
                    }
                }
                unitOfWork.MediaCategory.Update(ExistMediaCategory);
                unitOfWork.Complete();
                return Ok(ExistMediaCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a media category by its ID.
        /// </summary>
        /// <param name="id">The ID of the media category to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        /// <response code="204">If the media category is successfully deleted.</response>
        /// <response code="404">If the media category is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ExistMediaCategory = await unitOfWork.MediaCategory.GetByIdAsync(id);
                await RemoveImage(ExistMediaCategory.ImageName);
                unitOfWork.MediaCategory.Delete(ExistMediaCategory);
                unitOfWork.Complete();
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("GetImage/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads","Media", imageName);

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

            var filePath = Path.Combine("Uploads", "Media", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Media", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }
    }
}
