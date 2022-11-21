using Contact.Infrastructure.DataAccess.Seeder;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure.DataAccess.Context
{
    public class ContactContext : DbContext, IDataContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactContext).Assembly);
        }
    }
}
