using Kandil.Domain.Entities;
using Kandil.Infrastructure.Data;
using Kandil.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kandil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(_context);
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetContacts()
        {
            var Contacts = await unitOfWork.Contact.GetAllAsync();
            return Ok(Contacts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromForm] Contact contact)
        {
            await unitOfWork.Contact.AddAsync(contact);
            unitOfWork.Complete();  
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromForm] Contact updatedContact)
        {
            var contact =await  unitOfWork.Contact.GetByIdAsync(id);
            if (contact == null) return NotFound();

            contact.Name = updatedContact.Name;
            contact.Phone = updatedContact.Phone;
            contact.Email = updatedContact.Email;
            contact.Project = updatedContact.Project;
            contact.Message = updatedContact.Message;
            unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await unitOfWork.Contact.GetByIdAsync(id);

            unitOfWork.Contact.Delete(contact);
            if (contact == null) return NotFound();

            unitOfWork.Complete();  
            return NoContent();
        }
    }
}
