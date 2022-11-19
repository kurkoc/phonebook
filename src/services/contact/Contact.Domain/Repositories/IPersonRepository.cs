using BuildingBlocks.Domain;
using Contact.Domain.AggregateRoot;

namespace Contact.Domain.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> GetPerson(Guid id, CancellationToken cancellationToken); 
    }
}
