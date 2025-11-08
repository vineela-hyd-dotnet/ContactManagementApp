using Microsoft.EntityFrameworkCore;

namespace ContactManagementApp.Model
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }

    }
}
