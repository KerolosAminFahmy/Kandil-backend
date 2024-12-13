using kandil.Application.DTO;
using kandil.Domain.Entities;
using Kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishCategoryController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork work = new(_context);

        [HttpPost("AddFinishCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]

        public async Task<IActionResult> AddFinishCategory([FromForm] FinishCategoryDTO finishCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string imageName = Guid.NewGuid() + Path.GetExtension(finishCategoryDTO.Image.FileName);

                var filePath = Path.Combine("Uploads","Finish", imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await finishCategoryDTO.Image.CopyToAsync(stream);
                }
                var finishCategory = new FinishCategory
                {
                    ImageName = imageName,
                    Title = finishCategoryDTO.Title
                };
                var ct = work.FinishCategory.Add(finishCategory);
                work.Complete();
                return Ok(finishCategory);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }



        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllFinishCategory()
        {
            try
            {
                var Cities = await work.FinishCategory.GetAllAsync();
                return Ok(Cities);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }



        }

        [HttpGet("{id:int}", Name = "GetFinishCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFinishCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var finishCategory = work.FinishCategory.GetById(id);
            if (finishCategory == null)
            {
                return NotFound();
            }
            return Ok(finishCategory);
        }

        [HttpPut("{id:int}", Name = "UpdateFinishCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]

        public async Task<IActionResult> UpdateFinishCategory(int id, [FromForm] FinishCategoryDTO finishCategoryDTO)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var existingFinishCategory = await work.FinishCategory.GetByIdAsync(id);
            if (existingFinishCategory == null)
            {
                return NotFound();
            }

            if (finishCategoryDTO.Image != null)
            {

                if (!string.IsNullOrEmpty(existingFinishCategory.ImageName))
                {
                    var oldImagePath = Path.Combine("Uploads", "Finish", existingFinishCategory.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var newImageName = Guid.NewGuid() + Path.GetExtension(finishCategoryDTO.Image.FileName);
                var filePath = Path.Combine("Uploads", "Finish", newImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await finishCategoryDTO.Image.CopyToAsync(stream);
                }

                existingFinishCategory.ImageName = newImageName;
            }

            existingFinishCategory.Title = finishCategoryDTO.Title;
            work.FinishCategory.Update(existingFinishCategory);
            work.Complete();

            return Ok(existingFinishCategory);
        }

        [HttpDelete("{id:int}", Name = "DeleteFinishCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]

        public async Task<IActionResult> DeleteFinishCategory(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var finishCategory = work.FinishCategory.GetById(id);
            if (finishCategory == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(finishCategory.ImageName))
            {
                var imagePath = Path.Combine("Uploads", "Finish", finishCategory.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            work.FinishCategory.Delete(finishCategory);
            work.Complete();
            return Ok(finishCategory);
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "Finish", imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }
    }
}
