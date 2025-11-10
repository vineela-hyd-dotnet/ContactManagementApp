using ContactManagementApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementApp.Repository
{
    public class ContactRepo : IContactRepo
    {
        private readonly AppDbContext _context;


        public ContactRepo(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Contact> CreateContact(Contact contact)
        {

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact;


        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return false;
            }
            else
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return true;
            }

        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

      

        public async Task<Contact> GetContactById(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            var existing = await _context.Contacts.FindAsync(contact.Id);
            if (existing == null)
            {
                return null;
            }

            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;
            existing.Address = contact.Address;

            _context.Contacts.Update(existing);    
            await _context.SaveChangesAsync();     

            return existing;
        }

    }
}