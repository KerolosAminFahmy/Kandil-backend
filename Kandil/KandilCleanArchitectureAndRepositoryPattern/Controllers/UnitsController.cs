using kandil.Application.DTO;
using Kandil.Application.RepositoryInterfaces;
using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KandilCleanArchitectureAndRepositoryPattern.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        [HttpPost("AddUnit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddUnit([FromForm] UnitsDTO unitDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

          
                string mainImageName = await SaveImage(unitDto.Image);

                
               
                var unit = new Units
                {
                    ProjectId=unitDto.ProjectId,
                    Title = unitDto.Title,
                    Description = unitDto.Description,
                    ImageName = mainImageName,
                    status = unitDto.Status,
                    CodeUnit = unitDto.CodeUnit,
                    Area = unitDto.Area,
                    NumberBathroom = unitDto.NumberBathroom,
                    NumberRoom = unitDto.NumberRoom,
                    YearOfBuild = unitDto.YearOfBuild,
                    Price = unitDto.Price,
                    VideoUrl = unitDto.VideoUrl,
                    NameLocation = unitDto.NameLocation,
                    Longitude = unitDto.Longitude,
                    Latitude = unitDto.Latitude,
                    TypePrice = unitDto.TypePrice,
                };
                unit = await unitOfWork.Units.AddAsync(unit);
                unitOfWork.Complete();
                var detailImages = new List<UnitImage>();
                if (unitDto.DetailImage != null)
                {
                    foreach (var image in unitDto.DetailImage)
                    {
                        string imageName = await SaveImage(image);
                        detailImages.Add(new UnitImage { ImageName = imageName,UnitId=unit.Id });
                    }
                }
                await unitOfWork.UnitImage.AddRangeAsync(detailImages);

                var advantages = unitDto.Advantage.Select(a => new AdvantageUnit { Text = a,UnitId = unit.Id }).ToList();
                await unitOfWork.AdvantageUnit.AddRangeAsync(advantages);

                var services = unitDto.Services.Select(s => new ServiceUnit { Text = s, UnitId = unit.Id }).ToList();
                await unitOfWork.ServiceUnit.AddRangeAsync(services);
                unitOfWork.Complete();

                return Ok(unit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits()
        {
            var AllUnits = await unitOfWork.Units.GetAllAsync();
            return Ok(AllUnits);
        }
        [HttpGet("GetAllUnitHome")]
        public async Task<IActionResult> GetAllUnitHome()
        {
            var AllUnits = await unitOfWork.Units.FindAllAsync(e=>e.status== "Available");
            if (AllUnits != null)
            {
                if (AllUnits.Count() <= 15)
                {

                    return Ok(AllUnits);
                }
                return Ok(AllUnits.Take(15).OrderBy(e=>e.Id));
            }
            return Ok(AllUnits);
        }
        [HttpGet("{id:int}", Name =nameof(GetAllUnitsByProject))]
        public async Task<IActionResult> GetAllUnitsByProject(int id)
        {
            var AllUnits = await unitOfWork.Units.FindAllAsync(e=>e.ProjectId==id);
            return Ok(AllUnits);
        }
        
        [HttpGet("GetDetailUnits/{id:long}", Name = nameof(GetDetailUnits))]
        public async Task<IActionResult> GetDetailUnits(int id)
        {
            var Unit = await unitOfWork.Units.FindAsync(e=>e.Id==id);
            var AllService = await unitOfWork.ServiceUnit.FindAllAsync(e=>e.UnitId==id);
            var AllAdvantage = await unitOfWork.AdvantageUnit.FindAllAsync(e=>e.UnitId==id);
            var AllImage = await unitOfWork.UnitImage.FindAllAsync(e=>e.UnitId==id);

            var unit = new ShowUnitsDTO
            {
                Title = Unit.Title,
                status = Unit.status,
                ImageName = Unit.ImageName,
                serviceUnits = AllService,
                NameLocation = Unit.NameLocation,
                Longitude   = Unit.Longitude,
                Latitude = Unit.Latitude,
                TypePrice = Unit.TypePrice,
                advantageUnits = AllAdvantage,
                NumberBathroom = Unit.NumberBathroom,
                NumberRoom = Unit.NumberRoom,
                Description = Unit.Description,
                Price = Unit.Price, 
               
                CodeUnit = Unit.CodeUnit,
                unitImages = AllImage,
                VideoUrl = Unit.VideoUrl,
                YearOfBuild = Unit.YearOfBuild,
                Area= Unit.Area,    
                Id = Unit.Id,
               
            };


            return Ok(unit);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> UpdateUnit(UpdateUnitsDTO updateUnits)

        {
            var ExistUnit = await unitOfWork.Units.FindAsync(e=>e.Id==updateUnits.Id);
            if (ExistUnit == null)
            {
                return NotFound();
            }
            ExistUnit.Title=updateUnits.Title;
            ExistUnit.Price=updateUnits.Price;
            ExistUnit.status=updateUnits.Status;
            ExistUnit.NumberRoom=updateUnits.NumberRoom;
            ExistUnit.Description=updateUnits.Description;
            ExistUnit.Area=updateUnits.Area;
            ExistUnit.CodeUnit=updateUnits.CodeUnit;
            ExistUnit.NumberBathroom=updateUnits.NumberBathroom;
            ExistUnit.VideoUrl=updateUnits.VideoUrl;    
            ExistUnit.YearOfBuild=updateUnits.YearOfBuild;
            ExistUnit.Latitude=updateUnits.Latitude;
            ExistUnit.Longitude=updateUnits.Longitude;
            ExistUnit.NameLocation=updateUnits.NameLocation;
            ExistUnit.TypePrice=updateUnits.TypePrice;
            if (updateUnits.Image != null)
            {
                var isOK =await RemoveImage(ExistUnit.ImageName);
                if (isOK)
                {
                    var nameOfImage = await SaveImage(updateUnits.Image);
                    ExistUnit.ImageName = nameOfImage;  
                }
               

            }
            unitOfWork.Complete();
            if( updateUnits.AllRemovedAdvantage!=null && updateUnits.AllRemovedAdvantage.Count > 0)
            {
                foreach(var advantage in updateUnits.AllRemovedAdvantage)
                {
                    var obj = await unitOfWork.AdvantageUnit.FindAsync(e => e.Id == advantage);
                    unitOfWork.AdvantageUnit.Delete(obj);
                }
                unitOfWork.Complete();
            }
            if (updateUnits.AllRemovedServices != null && updateUnits.AllRemovedServices.Count > 0)
            {
                foreach (var service in updateUnits.AllRemovedServices)
                {
                    var obj = await unitOfWork.ServiceUnit.FindAsync(e => e.Id == service);
                    unitOfWork.ServiceUnit.Delete(obj);
                }
                unitOfWork.Complete();
            }
            if (updateUnits.AllRemovedImages != null && updateUnits.AllRemovedImages.Count > 0)
            {
                foreach (var Image in updateUnits.AllRemovedImages)
                {
                    var obj = await unitOfWork.UnitImage.FindAsync(e => e.Id == Image);
                    var isOk = RemoveImage(obj.ImageName);
                    unitOfWork.UnitImage.Delete(obj);
                }
                unitOfWork.Complete();
            }
            if(updateUnits.AdvantageUnits!=null && updateUnits.AdvantageUnits.Count > 0)
            {
                foreach(var advantage in updateUnits.AdvantageUnits)
                {
                    if (advantage.Id == 0)
                    {
                        var newAdvantage = new AdvantageUnit
                        {
                            Text = advantage.Text,
                            UnitId = updateUnits.Id,
                        };
                        unitOfWork.AdvantageUnit.Add(newAdvantage);
                    }
                    else
                    {
                        var existAdvantage=await unitOfWork.AdvantageUnit.FindAsync(e=>e.Id == advantage.Id);
                        if (existAdvantage != null) {
                            existAdvantage.Text = advantage.Text;
                        
                        }

                    }
                }

                unitOfWork.Complete();
            }
            if (updateUnits.ServiceUnits != null && updateUnits.ServiceUnits.Count > 0)
            {
                foreach (var service in updateUnits.ServiceUnits)
                {
                    if (service.Id == 0)
                    {
                        var newService = new ServiceUnit
                        {
                            Text = service.Text,
                            UnitId = updateUnits.Id,
                        };
                        unitOfWork.ServiceUnit.Add(newService);
                    }
                    else
                    {
                        var existService = await unitOfWork.ServiceUnit.FindAsync(e => e.Id == service.Id);
                        if (existService != null)
                        {
                            existService.Text = service.Text;

                        }

                    }
                }

                unitOfWork.Complete();
            }
            if (updateUnits.UnitImages != null && updateUnits.UnitImages.Count > 0) {
                
                foreach(var image in updateUnits.UnitImages)
                {
                    var nameOfImage = await SaveImage(image.Image);
                    var obj = new UnitImage
                    {
                        ImageName = nameOfImage,
                        UnitId = updateUnits.Id,
                    };
                    unitOfWork.UnitImage.Add(obj);  
                }
                unitOfWork.Complete();
               
            
            
            }
            
            return Ok();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var existUnit = await unitOfWork.Units.FindAsync(e=>e.Id == id);
            if (existUnit != null) {
                unitOfWork.Units.Delete(existUnit);
                unitOfWork.Complete();
            }

            return Ok();
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "Units", imageName);

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

            var filePath = Path.Combine("Uploads", "Units", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Units", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }

    }
   
}
