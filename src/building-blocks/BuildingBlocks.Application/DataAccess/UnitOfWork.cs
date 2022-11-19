using BuildingBlocks.Domain;

namespace BuildingBlocks.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _context;
        public UnitOfWork(IDataContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
