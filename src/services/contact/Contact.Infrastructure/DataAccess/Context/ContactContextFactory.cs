using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Contact.Infrastructure.DataAccess.Context
{
    internal class ContactContextFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        public ContactContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=PB-ContactDb;Username=postgres;Password=123456;");
            return new ContactContext(optionsBuilder.Options);
        }
    }
}
