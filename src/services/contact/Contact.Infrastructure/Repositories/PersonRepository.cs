using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.DataAccess;
using Contact.Domain.AggregateRoot;
using Contact.Domain.Repositories;
using Contact.Infrastructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure.Repositories
{
    public class PersonRepository : EfRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDataContext context) : base(context)
        {
        }

        public async Task<Person?> GetPerson(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(q => q.Contacts)
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        }
    }
}
