namespace BuildingBlocks.Domain
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> Query();
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
        void Add(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken);
        void Delete(TEntity entity);
        void DeleteMany(IEnumerable<TEntity> entities);
    }
}
