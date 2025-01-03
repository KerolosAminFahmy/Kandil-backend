using kandil.Application.DTO;
using Kandil.Application.DTO;
using Kandil.Application.RepositoryInterfaces;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace KandilCleanArchitectureAndRepositoryPattern.Web.Controllers
{
    /// <summary>
    /// Controller to manage city-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class CitiesController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly UnitOfWork work = new(dbContext);
        /// <summary>
        /// Adds a new city with the provided details.
        /// </summary>
        /// <param name="cityDTO">The data transfer object containing city details.</param>
        /// <returns>The newly created city object.</returns>
        /// <response code="200">City added successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Unauthorized access.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("AddCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        public async Task<IActionResult> AddCity([FromForm]CityDTO cityDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               string imageName = Guid.NewGuid() + Path.GetExtension(cityDTO.Image.FileName);

                var filePath = Path.Combine("Uploads", imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cityDTO.Image.CopyToAsync(stream);
                }
                var city = new City
                {
                    ImageName = imageName,
                    Name = cityDTO.Title
                };
                var ct=work.City.Add(city);
                work.Complete();
                return Ok(ct);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }
           
            

        }
        /// <summary>
        /// Retrieves all cities.
        /// </summary>
        /// <returns>A list of cities.</returns>
        /// <response code="200">Cities retrieved successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllCities()
        {
            try
            {
               var Cities = await work.City.GetAllAsync();
               return Ok(Cities);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }



        }
        /// <summary>
        /// Retrieves a specific city by ID.
        /// </summary>
        /// <param name="id">The ID of the city.</param>
        /// <returns>The city object.</returns>
        /// <response code="200">City retrieved successfully.</response>
        /// <response code="400">Invalid city ID.</response>
        /// <response code="404">City not found.</response>
        [HttpGet("{id:int}",Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCity(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var city =  await work.City.GetByIdAsync(id);
            if (city == null) {
                return NotFound(city);
            }
            return Ok(city);
        }
        [HttpGet("GetCityWithArea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCitiesWithArea()
        {
            var Cities = await work.City.GetAllAsync();
            List<CityWithAreaDTO>result = new List<CityWithAreaDTO>();

            foreach (var City in Cities) {
                CityWithAreaDTO re = new CityWithAreaDTO();
                re.city = City;
                var AllArea = await work.area.FindAllAsync(e=>e.CityId==City.Id);
                re.areas = AllArea;
                result.Add(re);
            }

            return Ok(result);
        }
        /// <summary>
        /// Updates a city's details.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="editCityDTO">The updated city details.</param>
        /// <returns>The updated city object.</returns>
        /// <response code="200">City updated successfully.</response>
        /// <response code="400">Invalid request or city ID.</response>
        /// <response code="401">Unauthorized access.</response>
        /// <response code="404">City not found.</response>
        [HttpPut("{id:int}", Name = "UpdateCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]

        public async Task<IActionResult> UpdateArea(int id, [FromForm] EditCityDTO editCityDTO)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var existingCity = await work.City.GetByIdAsync(id);
            if (existingCity == null)
            {
                return NotFound();
            }

            if (editCityDTO.Image != null)
            {

                if (!string.IsNullOrEmpty(existingCity.ImageName))
                {
                    var oldImagePath = Path.Combine("Uploads", existingCity.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var newImageName = Guid.NewGuid() + Path.GetExtension(editCityDTO.Image.FileName);
                var filePath = Path.Combine("Uploads", newImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await editCityDTO.Image.CopyToAsync(stream);
                }

                existingCity.ImageName = newImageName;
            }

            existingCity.Name = editCityDTO.Title;
            work.City.Update(existingCity);
            work.Complete();

            return Ok(existingCity);
        }
        /// <summary>
        /// Deletes a city by ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <returns>The deleted city object.</returns>
        /// <response code="200">City deleted successfully.</response>
        /// <response code="400">Invalid city ID.</response>
        /// <response code="404">City not found.</response>
        [HttpDelete("{id:int}",Name ="DeleteCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]

        public async Task<IActionResult> DeleteCity(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var city = await work.City.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound(city);
            }

            if (!string.IsNullOrEmpty(city.ImageName))
            {
                var imagePath = Path.Combine("Uploads", city.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            work.City.Delete(city);
            work.Complete();
            return Ok(city);
        }
        /// <summary>
        /// Retrieves an image by its name.
        /// </summary>
        /// <param name="imageName">The name of the image file.</param>
        /// <returns>The image file.</returns>
        /// <response code="200">Image retrieved successfully.</response>
        /// <response code="404">Image not found.</response>
        [HttpGet("{imageName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }
    }
}
