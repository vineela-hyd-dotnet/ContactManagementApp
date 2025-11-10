using ContactCompanyApi.Data;
using ContactCompanyApi.Models;
using ContactManagementApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactCompanyApi.Repositories
{
    public class ContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all contacts
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.Include(c => c.Company).ToListAsync();
        }

        // WHERE: Filter contacts by city
        public async Task<IEnumerable<Contact>> GetByCityAsync(string city)
        {
            return await _context.Contacts
                .Where(c => c.City.ToLower() == city.ToLower())
                .Include(c => c.Company)
                .ToListAsync();
        }

        // SELECT: Only name and email
        public async Task<IEnumerable<object>> GetNamesAndEmailsAsync()
        {
            return await _context.Contacts
                .Select(c => new { c.Name, c.Email })
                .ToListAsync();
        }

        // JOIN: Contacts with company name
        public async Task<IEnumerable<object>> GetContactsWithCompanyAsync()
        {
            return await _context.Contacts
                .Join(_context.Companies,
                      contact => contact.CompanyId,
                      company => company.Id,
                      (contact, company) => new
                      {
                          ContactName = contact.Name,
                          CompanyName = company.Name,
                          contact.Email,
                          contact.City
                      })
                .ToListAsync();
        }

        // GROUPBY: Count contacts per city
        public async Task<IEnumerable<object>> GetContactCountByCityAsync()
        {
            return await _context.Contacts
                .GroupBy(c => c.City)
                .Select(g => new { City = g.Key, Count = g.Count() })
                .ToListAsync();
        }

        // ORDERBY: Contacts ordered by name
        public async Task<IEnumerable<Contact>> GetOrderedByNameAsync()
        {
            return await _context.Contacts.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
