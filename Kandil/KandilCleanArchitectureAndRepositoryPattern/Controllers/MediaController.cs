using kandil.Application.DTO;
using kandil.Domain.Entities;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.Migrations;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        /// <summary>
        /// Creates a new media entry.
        /// </summary>
        /// <param name="dto">The media data transfer object.</param>
        /// <returns>The created media entry.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] MediaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdMedia = new Media
            {
                Created=dto.Created,
                Description=dto.Description,
                ImageName=await SaveImage(dto.Image),
                MediaId=dto.MediaId,
                Title=dto.Title,
                VideoURl=dto.videoURl,
            };
            unitOfWork.Media.Add(createdMedia);
            unitOfWork.Complete();
            var detailImages = new List<MediaImages>();
            if (dto.DetailImage != null)
            {
                foreach (var image in dto.DetailImage)
                {
                    string imageName = await SaveImage(image);
                    detailImages.Add(new MediaImages { ImageName = imageName,MediaId = createdMedia.Id });
                }
            }

            await unitOfWork.MediaImages.AddRangeAsync(detailImages);
            unitOfWork.Complete();
            return Ok(createdMedia);
        }

        /// <summary>
        /// Gets a media entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the media entry.</param>
        /// <returns>The media entry.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mediaObj = await unitOfWork.Media.GetByIdAsync(id);
            if (mediaObj == null)
                return NotFound();
            var media = new MediaDTO
            {
                Created = mediaObj.Created,
                Description = mediaObj.Description,
                Id = id,
                ImageName = mediaObj.ImageName,
                MediaId = mediaObj.MediaId,
                Title = mediaObj.Title,
                videoURl = mediaObj.VideoURl,
                
            };
            return Ok(media);
        }
        [HttpGet("ImagesMedia/{id}")]
        public async Task<IActionResult> GetImagesMedia(int id)
        {
            var AllImages = await unitOfWork.MediaImages.FindAllAsync(x => x.MediaId == id);
            var result = new List<MediaImages>();  
            foreach (var image in AllImages)
            {
                result.Add(new MediaImages
                {
                    Id = image.Id,
                    ImageName = image.ImageName,
                });
            }

            return Ok(result);

        }
        /// <summary>
        /// Gets all media entries.
        /// </summary>
        /// <returns>A list of media entries.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mediaList = await unitOfWork.Media.GetAllAsync();
            return Ok(mediaList);
        }
        [HttpGet("GetByMediaCategory/{id:int}")]
        public async Task<IActionResult> GetByMediaCategory(int id)
        {
            var mediaList = await unitOfWork.Media.FindAllAsync(e=>e.MediaId==id);

            return Ok(mediaList);
        }
        /// <summary>
        /// Updates a media entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the media entry.</param>
        /// <param name="dto">The updated media data transfer object.</param>
        /// <returns>The updated media entry.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromForm] MediaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ExistMedia = unitOfWork.Media.GetById(id);
                if (ExistMedia != null)
                {
                    ExistMedia.Title = dto.Title;
                    ExistMedia.Description = dto.Description;
                    ExistMedia.VideoURl = dto.videoURl; 
                    ExistMedia.Created  = dto.Created;
                    if(dto.Image != null)
                    {
                        var isOk = await RemoveImage(ExistMedia.ImageName);
                       

                        var nameOfImage = await SaveImage(dto.Image);
                        ExistMedia.ImageName = nameOfImage;
                        
                    }
                    if(dto.AllRemovedImages != null && dto.AllRemovedImages.Count() > 0)
                    {
                        foreach (var image in dto.AllRemovedImages)
                        {
                            MediaImages obj= await unitOfWork.MediaImages.GetByIdAsync(image);

                            bool isOk=await RemoveImage(obj.ImageName);
                            unitOfWork.MediaImages.Delete(obj);


                        }
                        unitOfWork.Complete();

                    }
                    var detailImages = new List<MediaImages>();
                    if (dto.DetailImage != null)
                    {
                        foreach (var image in dto.DetailImage)
                        {
                            string imageName = await SaveImage(image);
                            detailImages.Add(new MediaImages { ImageName = imageName, MediaId = ExistMedia.Id });
                        }
                    }
                    await unitOfWork.MediaImages.AddRangeAsync(detailImages);
                    unitOfWork.Complete();
                }
                unitOfWork.Media.Update(ExistMedia);
                unitOfWork.Complete();

                return Ok(ExistMedia);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a media entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the media entry.</param>
        /// <returns>No content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ExistMedia = await unitOfWork.Media.GetByIdAsync(id);
                await RemoveImage(ExistMedia.ImageName);
                unitOfWork.Media.Delete(ExistMedia);
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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "Media", imageName);

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
