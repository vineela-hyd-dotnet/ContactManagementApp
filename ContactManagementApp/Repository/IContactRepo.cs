using ContactManagementApp.Model;

namespace ContactManagementApp.Repository
{
    public interface IContactRepo
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> UpdateContact(Contact contact);
        Task<bool> DeleteContact(int id);
    }
}
