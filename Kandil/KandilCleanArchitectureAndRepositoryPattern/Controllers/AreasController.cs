using kandil.Application.DTO;
using Kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KandilCleanArchitectureAndRepositoryPattern.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);

        [HttpPost("AddArea")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> AddArea([FromForm] AreaDTO areaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(areaDTO.Image.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("امتداد الصورة غير مدعوم.");
            }

            try
            {
                var imageName = Guid.NewGuid() + fileExtension;
                var filePath = Path.Combine("Uploads", imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await areaDTO.Image.CopyToAsync(stream);
                }

                var area = new area
                {
                    Name = areaDTO.Name,
                    ImageName = imageName,
                    CityId = areaDTO.CityId
                };

                await unitOfWork.area.AddAsync(area);
                unitOfWork.Complete();

                return Ok(area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "حدث خطأ أثناء العملية.");
            }
        }
        [HttpGet("GetAllAreas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAreas()
        {
            try
            {
                var areas = unitOfWork.area.FindAll(e => e.Id >= 0, ["City"]);
                return Ok(areas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("AllAreas")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> AllAreas()
        {
            var result = await _context.Cities.AsNoTracking()
             .Select(city => new AllAreaDTO
             {
                 Id = city.Id,
                 City = city.Name,
                 ViewAreas = city.Areas.Select(area => new ViewAreaDTO
                 {
                     id = area.Id,
                     name = area.Name,
                     ImageName = area.ImageName
                 }).ToList() 
             })
             .ToListAsync();


            return Ok(result);
        }
        [HttpGet("{id:int}", Name = "GetArea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetArea(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await unitOfWork.area.FindAllAsync(e => e.Id == id, ["City"]); 
            if (area == null)
            {
                return NotFound();
            }

            return Ok(area);
        }
        [HttpGet("GetAreaByCity/{id:int}", Name = "GetAreaByCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAreaByCity(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await unitOfWork.area.FindAllAsync(e => e.CityId == id);
           
            
            if (area == null)
            {
                return NotFound();
            }
            area.ElementAt(0).City = await unitOfWork.City.GetByIdAsync(id);
            return Ok(area);
        }
        [HttpPut("{id:int}", Name = "UpdateArea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]

        public async Task<IActionResult> UpdateArea(int id, [FromForm] EditAreaDTO areaDTO)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var existingArea = await unitOfWork.area.GetByIdAsync(id);
            if (existingArea == null)
            {
                return NotFound();
            }

            if (areaDTO.Image != null)
            {

                if (!string.IsNullOrEmpty(existingArea.ImageName))
                {
                    var oldImagePath = Path.Combine("Uploads", existingArea.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var newImageName = Guid.NewGuid() + Path.GetExtension(areaDTO.Image.FileName);
                var filePath = Path.Combine("Uploads", newImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await areaDTO.Image.CopyToAsync(stream);
                }

                existingArea.ImageName = newImageName;
            }

            existingArea.Name = areaDTO.Name;
            existingArea.CityId = areaDTO.CityId;

            unitOfWork.area.Update(existingArea);
            unitOfWork.Complete();

            return Ok(existingArea);
        }


        [HttpDelete("{id:int}", Name = "DeleteArea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await unitOfWork.area.GetByIdAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(area.ImageName))
            {
                var imagePath = Path.Combine("Uploads", area.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            unitOfWork.area.Delete(area);
            unitOfWork.Complete();

            return Ok(area);
        }

    }
}
