using ContactManagementApp.Model;
using ContactManagementApp.Repository;

namespace ContactManagementApp.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepo _contactRepo;
        public ContactService(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo ?? throw new ArgumentNullException(nameof(contactRepo));
        }
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
           return await _contactRepo.CreateContact(contact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
           return await _contactRepo.DeleteContact(id);
        }

        public Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return _contactRepo.GetAllContacts();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await (_contactRepo.GetContactById(id));
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            if (contact.Id == 0)
            {
                throw new Exception("Invalid Contact id");
            }
            return await _contactRepo.UpdateContact(contact);
        }
    }
}
