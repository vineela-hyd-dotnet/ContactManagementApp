using ContactManagementApp.Model;
using ContactManagementApp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

      
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contacts = await _contactService.GetAllContactsAsync();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all contacts.", ex);
            }
        }

    
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                
                var contact = await _contactService.GetContactByIdAsync(id);
                if (contact == null)
                    return NotFound($"Contact with ID {id} not found.");

                return Ok(contact);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving contact with ID {id}.", ex);
            }
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            try
            {
                if (contact == null)
                    return BadRequest("Invalid contact data.");

                var createdContact = await _contactService.CreateContactAsync(contact);
                return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the contact.", ex);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating contact with ID {id}.", ex);
            }
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _contactService.DeleteContactAsync(id);

                if (!result)
                    return NotFound($"Contact with ID {id} not found or could not be deleted.");

                return Ok(new { message = "Contact deleted successfully." });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting contact with ID {id}.", ex);
            }
        }
    }
}
