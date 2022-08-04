using Framework.Core.Abstractions;
using Framework.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Seisankanri.Business.Core
{
    public class CoreRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Public Constructors
        public CoreRepository(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion Public Constructors


        private void FillCreateInfo(TEntity entity)
        {
            entity.created_at = DateTime.UtcNow;
            // TODO: update user id
            entity.created_by = Guid.NewGuid().ToString(); // this.serviceContext.UserFullName;
        }

        private void FillUpdateInfo(TEntity entity)
        {
            entity.updated_at = DateTime.UtcNow;
            // TODO: update user id
            entity.updated_by = Guid.NewGuid().ToString();// this.serviceContext.UserFullName;
        }
    }
}