using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.DataAccess
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly IDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public EfRepository(IDataContext context)
        {
            _context = context;
            _dbSet = (_context as DbContext).Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
