using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Data.Models;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext context;

        public ContactController(ContactContext _context)
        {
            context = _context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            var contacts = await context.Contacts.ToListAsync();
            return contacts;            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null) return NotFound();

            return contact;
        }

        [HttpPost]
        public async Task<IActionResult> PostContact(Contact request)
        {
            context.Contacts.Add(request);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new { id = request.Id}, request);
        }

        [HttpPut]
        public async Task<IActionResult> PutContact(Contact request)
        {
            var existingContact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingContact == null) return NotFound();

            existingContact.Firstname = request.Firstname;
            existingContact.Lastname = request.Lastname;
            existingContact.ContactNumber = request.ContactNumber;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var existingContact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (existingContact == null) return NotFound();

            context.Contacts.Remove(existingContact);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}