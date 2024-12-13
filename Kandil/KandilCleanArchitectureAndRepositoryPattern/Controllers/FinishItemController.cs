using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishItemController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork work = new(_context);



    }
}
