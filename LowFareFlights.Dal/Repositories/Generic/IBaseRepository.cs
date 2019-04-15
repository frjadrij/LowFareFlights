using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.Repositories.Generic
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetEntity();

        IQueryable<TEntity> GetAllAsQueryable();
        IQueryable<TEntity> GetAllAsQueryableAsNoTracking();

        IEnumerable<TEntity> GetAllAsEnumerable();
        IEnumerable<TEntity> GetAllAsEnumerableAsNoTracking();

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(params object[] keys);
        Task<TEntity> FindAsync(params object[] keys);

        void AddToDatabase(TEntity entity);
        void AddRangeToDatabase(IEnumerable<TEntity> entities);

        void UpdateInDatabase(TEntity entity, int id);
        void UpdateInDatabase(TEntity entity, Guid guid);

        void AddOrUpdateInDatabase(TEntity entity, int id);
        void AddOrUpdateInDatabase(TEntity entity, Guid guid);

        void DeleteFromDatabase(int id);
        void DeleteFromDatabase(Guid guid);
        void DeleteFromDatabase(TEntity entity);
        void DeleteRangeFromDatabase(IEnumerable<TEntity> entities);

        void AttachEntity(TEntity entity);
        void SetEntityState(TEntity entity, EntityState entityState);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
