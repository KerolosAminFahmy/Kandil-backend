using kandil.Application.DTO;
using Kandil.Application.DTO;
using Kandil.Application.RepositoryInterfaces;
using System.IO;
using Microsoft.AspNetCore.Http;

using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace KandilCleanArchitectureAndRepositoryPattern.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        [HttpPost("AddProject")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> AddProject([FromForm]ProjectDTO projectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NameMainImage = await SaveImage(projectDTO.MainImage);
            string NameLocationImage = null;
            if(projectDTO.LocationImage != null)
            {
                NameLocationImage = await SaveImage(projectDTO.LocationImage);
            }
            string PdfName = null;
            if (projectDTO.pdfFile != null)
            {
                PdfName = await SaveImage(projectDTO.pdfFile);
            } 
            
            var project = new Project
            {
                AboutProject = projectDTO.AboutProject,
                AreaId = projectDTO.AreaId,
                ImageName= NameMainImage,
                VideoUrl = projectDTO.VideoUrl,
                ImageNameLocation = NameLocationImage,
                Title = projectDTO.Title,
                PdfName = PdfName,
            };

            project=await unitOfWork.Project.AddAsync(project);
            //unitOfWork.Complete();
            if(projectDTO.Images != null && projectDTO.Images.Count() != 0)
            {
                foreach (var detailImage in projectDTO.Images)
                {
                    var imageName = await SaveImage(detailImage.Image);
                    var projectImage = new ProjectImage
                    {
                        ImageName = imageName,
                        ProjectId = project.Id
                    };
                    await unitOfWork.ProjectImages.AddAsync(projectImage);
                }
            }
            if(projectDTO.advantageProjects != null && projectDTO.advantageProjects.Count() != 0)
            {
                foreach (var advantage in projectDTO.advantageProjects)
                {
                    var imageName = await SaveImage(advantage.Image);
                    var advantageProject = new AdvantageProject
                    {
                        Text = advantage.Text,
                        ImageUrl = imageName,
                        ProjectId = project.Id
                    };
                    await unitOfWork.AdvantageProjects.AddAsync(advantageProject);
                }
            }
            if (projectDTO.locationProjects != null && projectDTO.locationProjects.Count() != 0)
            {
                foreach (var location in projectDTO.locationProjects)
                {
                    var locationProject = new LocationProject
                    {
                        Time = location.Time.ToString(),
                        NameOfStreet = location.Street,
                        ProjectId = project.Id
                    };
                    await unitOfWork.LocationProjects.AddAsync(locationProject);
                }
            } 
            unitOfWork.Complete();

            
            return Ok();
        }

        [HttpGet(nameof(GetProjectsWithArea))]
        public async Task<IActionResult> GetProjectsWithArea()
        {
            var result = await _context.Areas.AsNoTracking()
           .Select(area => new AllProjectDTO
           {
               Id = area.Id,
               AreaName = area.Name,
               ViewProject = area.projects.Select(project => new ViewProjectDTO
               {
                   id = project.Id,
                   name = project.Title,
                   ImageName = project.ImageName
               }).ToList()
           })
           .ToListAsync();


            return Ok(result);

        }

        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetProjects()
        {
            List<ShowProjectDTO> showProjectDTOs = new List<ShowProjectDTO>();
            var projects = await unitOfWork.Project.GetAllAsync();
   
            if (projects == null)
            {
                return NotFound("المشروع غير موجود.");
            }
            foreach(var project in projects)
            {
                var sh = new ShowProjectDTO
                {
                    Id=project.Id,
                    Name = project.Title,
                    Image = project.ImageName,
                };
                showProjectDTOs.Add(sh);
            }

            return Ok(showProjectDTOs);
        }
        [HttpGet("{id:int}", Name = "GetAllProject")]
        public async Task<IActionResult> GetDetailProject(int id)
        {
            //var DetailImage = await unitOfWork.ProjectImages.FindAllAsync(x => x.ProjectId == id);
            //var Advantage   = await unitOfWork.AdvantageProjects.FindAllAsync(x=>x.ProjectId == id);
            //var Location    = await unitOfWork.LocationProjects.FindAllAsync(x=>x.ProjectId==id);
            //var project     = await unitOfWork.Project.GetByIdAsync(id);
            //var detailProject = new DetailProjectDTO
            //{
            //    AboutProject = project.AboutProject,
            //    AdvantageProjects = Advantage,
            //    LocationImage = project.ImageNameLocation,
            //    MainImage = project.ImageName,
            //    ProjectImages = DetailImage,
            //    Title = project.Title,
            //    VideoUrl = project.VideoUrl,
            //    LocationProjects = Location,


            //};
            var result1 = _context.Projects.FirstOrDefault(e => e.Id == id);
            var result = new ViewUpdateDTO
            {
                AboutProject = result1.AboutProject,
                LocationImage = result1.ImageNameLocation,
                MainImage = result1.ImageName,
                Title = result1.Title,
                PdfFile=result1.PdfName,
                VideoURL = result1.VideoUrl,
                advantageProjects = _context.AdvantageProjects.Where(e => e.ProjectId == id).ToList(),
                LocationProjects = _context.LocationProjects.Where(e => e.ProjectId == id).ToList(),
                Images = _context.projectImages.Where(e => e.ProjectId == id).Select(e => e.ImageName).ToList(),
            };


            return Ok(result);
        }
        [HttpGet("GetProjectDetail/{id:int}", Name = "GetProjectDetail")]
        public async Task<IActionResult> GetProjectDetail(int id)
        {
            var DetailImage = await unitOfWork.ProjectImages.FindAllAsync(x => x.ProjectId == id);
            var Advantage = await unitOfWork.AdvantageProjects.FindAllAsync(x => x.ProjectId == id);
            var Location = await unitOfWork.LocationProjects.FindAllAsync(x => x.ProjectId == id);
            var project = await unitOfWork.Project.GetByIdAsync(id);
            var detailProject = new DetailProjectDTO
            {
                AboutProject = project.AboutProject,
                AdvantageProjects = Advantage,
                LocationImage = project.ImageNameLocation,
                MainImage = project.ImageName,
                ProjectImages = DetailImage,
                Title = project.Title,
                VideoUrl = project.VideoUrl,
                LocationProjects = Location,


            };


            return Ok(detailProject);
        }

        [HttpGet("GetAllProjectByArea/{id:int}", Name = "GetAllProjectByArea")]
        public async Task<IActionResult> GetAllProjectByArea(int id)
        {
          var project = await unitOfWork.Project.FindAllAsync(e => e.AreaId == id, ["Area"]);

            List<Project> result = new List<Project>();
            foreach (var item in project)
            {
                var units = await unitOfWork.Units.FindAllAsync(e => e.ProjectId == item.Id);
                if (units == null || units.Count() == 0)
                {
                    result.Add(item);
                    continue;
                }
                bool isPaid = false;
                foreach (var unit in units)
                {
                    if (unit.status.Equals("Available"))
                    {
                        isPaid = true;
                        break;
                    }

                }
                if (isPaid)
                {
                    result.Add(item);


                }
            }
            return Ok(result);
        }
        [HttpGet("GetAllPaidProjectByArea/{id:int}", Name = "GetAllPaidProjectByArea")]
        public async Task<IActionResult> GetAllPaidProjectByArea(int id)
        {
            var project = await unitOfWork.Project.FindAllAsync(e => e.AreaId == id, ["Area"]);
            List<Project> result = new List<Project>();
            foreach (var item in project)
            {
                var units = await unitOfWork.Units.FindAllAsync(e => e.ProjectId == item.Id);
                if (units == null || units.Count() == 0)
                {
                    continue;
                }
                bool isPaid = true;
                foreach (var unit in units)
                {
                    if (unit.status.Equals("Available"))
                    {
                        isPaid = false;
                        break;
                    }

                }
                if (isPaid) {
                    result.Add(item);


                }
            }
            return Ok(result);
        }
        [HttpGet("GetCityName/{id:int}")]
        public async Task<IActionResult> GetCityName(int id)
        {
            var project = await unitOfWork.City.GetByIdAsync(id);
            if (project == null)
                return NotFound();
            string name = project.Name;
            return Ok(new { message = name });
        }
        [HttpGet("GetAreaName/{id:int}")]
        public async Task<IActionResult> GetAreaName(int id)
        {
            var area = await unitOfWork.area.GetByIdAsync(id);
            if (area == null)
                return NotFound();
            string name = area.Name;
            return Ok(new { message = name });
        }
        [HttpGet("GetProjectName/{id:int}")]
        public async Task<IActionResult> GetProjectName(int id)
        {
            var area = await unitOfWork.Project.GetByIdAsync(id);
            if (area == null)
                return NotFound();
            string name = area.Title;
            return Ok(new { message = name });
        }
        [HttpDelete("{id:int}",Name = "DeleteProject")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            await RemoveImage(project.ImageName);    
            await RemoveImage(project.ImageNameLocation);
            await RemoveImage(project.PdfName);
            unitOfWork.Project.Delete(project);
            unitOfWork.Complete();

            return Ok();
        }



        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads","Projects", imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> UpdateProject([FromForm]  ProjectUpdateDTO projectUpdate)
        {
            var existProject=unitOfWork.Project.GetById(projectUpdate.ProjectId);
            if (existProject == null) {
                return NotFound(); 
            }
            if (projectUpdate.pdfFile != null) {
                var isOk = await RemoveImage(existProject.PdfName);
                if (isOk)
                {
                    existProject.PdfName = await SaveImage(projectUpdate.pdfFile);
                }

            }
            if (projectUpdate.MainImage != null) {
               var isOk =  await RemoveImage(existProject.ImageName);
                if (isOk) {
                 existProject.ImageName = await SaveImage(projectUpdate.MainImage);
                }
            }
            if (projectUpdate.LocationImage != null) {
                var isOk = await RemoveImage(existProject.ImageNameLocation);
                if (isOk)
                {
                    existProject.ImageNameLocation = await SaveImage(projectUpdate.LocationImage);
                }
            }
            existProject.Title = projectUpdate.Title;
            existProject.VideoUrl=projectUpdate.VideoURL;
            existProject.AboutProject=projectUpdate.AboutProject;
            unitOfWork.Project.Update(existProject);
            unitOfWork.Complete();
            if (projectUpdate.ImageRemoved!=null && projectUpdate.ImageRemoved.Count() != 0) {
                foreach (var image in projectUpdate.ImageRemoved)
                {
                    await RemoveImage(image);
                    unitOfWork.ProjectImages.Delete(await unitOfWork.ProjectImages.FindAsync(e => e.ImageName == image));

                }
                unitOfWork.Complete();

            }
            if (projectUpdate.images != null) {
                foreach (var detailImage in projectUpdate.images)
                {
                    var imageName = await SaveImage(detailImage.Image);
                    var projectImage = new ProjectImage
                    {
                        ImageName = imageName,
                        ProjectId = existProject.Id
                    };
                    await unitOfWork.ProjectImages.AddAsync(projectImage);
                }
                unitOfWork.Complete();

            }
            if (projectUpdate.LocationRemovedList != null ) { 
               foreach(var location in projectUpdate.LocationRemovedList)
                {
                    var removeLocation=await unitOfWork.LocationProjects.FindAsync(e => e.Id == location);
                    unitOfWork.LocationProjects.Delete(removeLocation);

                }
               unitOfWork.Complete();
            }
            if (projectUpdate.AdvantageRemovedList != null)
            {
                foreach (var Advantage in projectUpdate.AdvantageRemovedList)
                {
                    var removeAdvantage = await unitOfWork.AdvantageProjects.FindAsync(e => e.Id == Advantage);
                    unitOfWork.AdvantageProjects.Delete(removeAdvantage);

                }
                unitOfWork.Complete();
            }
            if (projectUpdate.LocationProjects != null) {
                foreach(var location in projectUpdate.LocationProjects)
                {
                    if (location.id == 0)

                    {
                        var locationProject = new LocationProject
                        {
                            Time = location.time.ToString(),
                            NameOfStreet = location.nameOfStreet,
                            ProjectId = existProject.Id
                        };
                        await unitOfWork.LocationProjects.AddAsync(locationProject);
                    }
                    else
                    {
                        var existProjectLocation = unitOfWork.LocationProjects.Find(e => e.Id == location.id);
                    
                        existProjectLocation.NameOfStreet = location.nameOfStreet;
                        existProjectLocation.Time = location.time;  

                    }
                }
                unitOfWork.Complete();

            }
            if (projectUpdate.advantageUpdates != null)
            {
                foreach (var advantage in projectUpdate.advantageUpdates)
                {
                    if (advantage.id == 0)

                    {
                        
                            var imageName = await SaveImage(advantage.Image);
                            var advantageProject = new AdvantageProject
                            {
                                Text = advantage.Text,
                                ImageUrl = imageName,
                                ProjectId = existProject.Id,
                            };
                            await unitOfWork.AdvantageProjects.AddAsync(advantageProject);
                        
                    }
                    else
                    {
                        var existProjectAdvantage = unitOfWork.AdvantageProjects.Find(e => e.Id == advantage.id);

                        existProjectAdvantage.Text = advantage.Text;
                        if(existProjectAdvantage.ImageUrl != advantage.Image.FileName)
                        {
                            var isOk = await RemoveImage(existProjectAdvantage.ImageUrl);
                            if (isOk)
                            {
                                existProjectAdvantage.ImageUrl = await SaveImage(advantage.Image);
                            }

                        }

                    }
                }
                unitOfWork.Complete();

            }
            return Ok();
        }

        private async Task<string> SaveImage(IFormFile Imgae)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(Imgae.FileName);

            var filePath = Path.Combine("Uploads","Projects", imageName);

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
                var oldImagePath = Path.Combine("Uploads", "Projects", ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                     System.IO.File.Delete(oldImagePath);
                }
            }
            return true;

        }


    }
}
