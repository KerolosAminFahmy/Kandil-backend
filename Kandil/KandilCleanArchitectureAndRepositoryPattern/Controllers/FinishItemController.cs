using kandil.Application.DTO;
using kandil.Domain.Entities;
using Kandil.Application.DTO;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishItemController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork work = new(_context);

        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        [HttpPost("AddFinishItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddFinishItem([FromForm] FinishItemDTO finishItemDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                string mainImageName = await SaveImage(finishItemDTO.Image);



                var finishItem = new FinishItem
                {
                    FinishCategoryId = finishItemDTO.FinishCategoryId,
                    Title = finishItemDTO.Title,
                    Description = finishItemDTO.Description,
                    ImageName = mainImageName,
                    VideoUrl = finishItemDTO.VideoUrl,
                    NameLocation = finishItemDTO.NameLocation,
                    Longitude = finishItemDTO.Longitude,
                    Latitude = finishItemDTO.Latitude,
                   
                };
                finishItem = await unitOfWork.FinishItem.AddAsync(finishItem);
                unitOfWork.Complete();
                var detailImages = new List<FinishImage>();
                if (finishItemDTO.DetailImage != null)
                {
                    foreach (var image in finishItemDTO.DetailImage)
                    {
                        string imageName = await SaveImage(image);
                        detailImages.Add(new FinishImage { ImageName = imageName, FinishItemId = finishItem.Id });
                    }
                }
                await unitOfWork.FinishImage.AddRangeAsync(detailImages);
                unitOfWork.Complete();

                return Ok(finishItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getFinishItem/{id:int}", Name = nameof(GetAllFinishItem))]
        public async Task<IActionResult> GetAllFinishItem(int id)
        {
            var AllFinishItem = await unitOfWork.FinishItem.FindAllAsync(e => e.FinishCategoryId == id);
            return Ok(AllFinishItem);
        }
        [HttpGet("getAllFinishItemWithName/{id:int}", Name = nameof(GetAllFinishItemWithName))]

        public async Task<IActionResult> GetAllFinishItemWithName(int id)
        {
            var AllFinishItem = await unitOfWork.FinishItem.FindAllAsync(e => e.FinishCategoryId == id);
            var nameFinish = await unitOfWork.FinishCategory.FindAsync(e=>e.Id== id);
            return Ok(new { name = nameFinish.Title, data = AllFinishItem });
        }

        [HttpGet("getDetailFinishItem/{id:int}", Name = nameof(GetDetailFinishItem))]
        public async Task<IActionResult> GetDetailFinishItem(int id)
        {
            var FinishItem = await unitOfWork.FinishItem.FindAsync(e => e.Id == id);
            var AllImage = await unitOfWork.FinishImage.FindAllAsync(e => e.FinishItemId == id);

            var finish = new FinishItemDetailDTO
            {
                finishItem = FinishItem,
                finishImages = AllImage
            };


            return Ok(finish);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> UpdateUnit(FinishItemDTO updateFinishItem)

        {
            var ExistFinishItem = await unitOfWork.FinishItem.FindAsync(e => e.Id == updateFinishItem.Id);
            if (ExistFinishItem == null)
            {
                return NotFound();
            }
            ExistFinishItem.Title = updateFinishItem.Title;
            ExistFinishItem.Description = updateFinishItem.Description;
            ExistFinishItem.VideoUrl = updateFinishItem.VideoUrl;
            ExistFinishItem.Latitude = updateFinishItem.Latitude;
            ExistFinishItem.Longitude = updateFinishItem.Longitude;
            ExistFinishItem.NameLocation = updateFinishItem.NameLocation;
            if (updateFinishItem.Image != null)
            {
                var isOK = await RemoveImage(ExistFinishItem.ImageName);
                
                var nameOfImage = await SaveImage(updateFinishItem.Image);
                ExistFinishItem.ImageName = nameOfImage;
                


            }
            unitOfWork.Complete();
            if (updateFinishItem.AllRemovedImages != null && updateFinishItem.AllRemovedImages.Count > 0)
            {
                foreach (var Image in updateFinishItem.AllRemovedImages)
                {
                    var obj = await unitOfWork.FinishImage.FindAsync(e => e.Id == Image);
                    var isOk = RemoveImage(obj.ImageName);
                    unitOfWork.FinishImage.Delete(obj);
                }
                unitOfWork.Complete();
            }
            if (updateFinishItem.DetailImage != null && updateFinishItem.DetailImage.Count() > 0)
            {

                foreach (var image in updateFinishItem.DetailImage)
                {
                    var nameOfImage = await SaveImage(image);
                    var obj = new FinishImage
                    {
                        ImageName = nameOfImage,
                        FinishItemId = (int)updateFinishItem.Id,
                    };
                    unitOfWork.FinishImage.Add(obj);
                }
                unitOfWork.Complete();



            }

            return Ok();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteFinishItem(int id)
        {
            var existFinishItem = await unitOfWork.FinishItem.FindAsync(e => e.Id == id);
            if (existFinishItem != null)
            {
                unitOfWork.FinishItem.Delete(existFinishItem);
                unitOfWork.Complete();
            }

            return Ok();
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
        private async Task<string> SaveImage(IFormFile Imgae)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(Imgae.FileName);

            var filePath = Path.Combine("Uploads", "Finish", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Finish", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }


    }
}
