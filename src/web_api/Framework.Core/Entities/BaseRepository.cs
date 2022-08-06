using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Core.Entities
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>
    {
        protected DbContext DbContext { get; private set; }
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IServiceContext _serviceContext;

        public BaseRepository(DbContext dbContext, IServiceContext serviceContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<TEntity>();
            _serviceContext = serviceContext;
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            entity.id = Guid.NewGuid();
            FillCreateInfo(entity);
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteEntityAsync(TEntity entity)
        {
            entity.del_flg = true;
            FillUpdateInfo(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            var result = _dbSet.Any(x => x.id == id);
            return await Task.FromResult(result);
        }

        public async Task<TEntity> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetQuery(Func<DbSet<TEntity>, IQueryable<TEntity>>? func = null)
        {
            return func != null ? func(_dbSet) : _dbSet;
        }

        public async Task UpdateEntityAsync(TEntity entity)
        {
            DbContext.Attach(entity);
            FillUpdateInfo(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        private void FillCreateInfo(TEntity entity)
        {
            entity.created_date = DateTime.UtcNow;
            entity.created_by = Guid.NewGuid(); //(Guid)_serviceContext._userId;
        }

        private void FillUpdateInfo(TEntity entity)
        {
            entity.updated_date = DateTime.UtcNow;
            entity.updated_by = Guid.NewGuid(); //_serviceContext._userId;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }
    }
}
