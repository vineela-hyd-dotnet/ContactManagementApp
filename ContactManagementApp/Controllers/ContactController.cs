using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManagementApp.Model;
using ContactManagementApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // ✅ GET: api/contact/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        // ✅ GET: api/contact/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound($"Contact with ID {id} not found.");

            return Ok(contact);
        }

        // ✅ POST: api/contact
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest("Invalid contact data.");

            var createdContact = await _contactService.CreateContactAsync(contact);

            return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
        }

        // ✅ PUT: api/contact/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest("Invalid contact data.");

            if (id != contact.Id)
                return BadRequest("Contact ID mismatch.");

            var existing = await _contactService.GetContactByIdAsync(id);
            if (existing == null)
                return NotFound($"Contact with ID {id} not found.");

            await _contactService.UpdateContactAsync(contact);
            return Ok(new { message = "Contact updated successfully." });
        }

        // ✅ DELETE: api/contact/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);

            if (!result)
                return NotFound($"Contact with ID {id} not found or could not be deleted.");

            return Ok(new { message = "Contact deleted successfully." });
        }
    }
}
