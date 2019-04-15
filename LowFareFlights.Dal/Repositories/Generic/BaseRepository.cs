using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Dal.Repositories.Generic
{
    /// <summary>
    /// Generic Base Repository with custom DbSet metods for database calls, 
    /// setting Entity States for CRUD and calling DbContext.SaveChanges().
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity type, placeholder for concrete types.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The Entity DbSet.
        /// </summary>
        private readonly DbSet<TEntity> _entities;

        /// <summary>
        /// The Repository DbContext.
        /// </summary>
        protected readonly DbContext _dbContext;

        /// <summary>
        /// The Generic Repository Constructor.
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }


        /// <summary>
        /// Lambda method that returns Entity DbSet.
        /// </summary>
        /// <returns></returns>
        public DbSet<TEntity> GetEntity() => _entities;


        /// <summary>
        /// Lambda method that returns IQueryable DbSet Entity records.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllAsQueryable() => _entities.AsQueryable();

        /// <summary>
        /// Lambda that returns IQueryable DbSet Entity records without cacheing.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllAsQueryableAsNoTracking() => _entities.AsNoTracking().AsQueryable();


        /// <summary>
        /// Lambda method that returns IEnumerable DbSet Entity records.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAllAsEnumerable() => _entities.AsEnumerable();

        /// <summary>
        /// Lambda method that returns IEnumerable DbSet Entity records without cacheing.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAllAsEnumerableAsNoTracking() => _entities.AsNoTracking().AsEnumerable();


        /// <summary>
        /// Expresion parametrized lambda method that returns first DbSet Entity record from the sequence thet satisfies boolean condition or a default value.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => _entities.FirstOrDefault(predicate);

        /// <summary>
        /// Expresion parametrized lambda method that asynchronously returns first DbSet Entity record from the sequence thet satisfies boolean condition or a default value.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.FirstOrDefaultAsync(predicate);


        /// <summary>
        /// Lambda method that returns DbSet Entity record based on primary key value passed as parameter in array of key values.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public TEntity Find(params object[] keys) => _entities.Find(keys);

        /// <summary>
        /// Lambda method that asynchronously returns DbSet Entity record based on primary key value passed as parameter in array of key values.
        /// <see cref="Task<TEntity> FindAsync(params object[] keyValues)"/>
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(params object[] keys) => await _entities.FindAsync(keys);


        /// <summary>
        /// Lambda method for setting DbSet Entity to Added state in DbContext.
        /// </summary>
        /// <param name="entity"></param>
        public void AddToDatabase(TEntity entity) => _entities.Add(entity);

        /// <summary>
        /// Lambda method for setting IEnumerable DbSet Entity collection to Added state in DbContext.
        /// </summary>
        /// <param name="entities"></param>
        public void AddRangeToDatabase(IEnumerable<TEntity> entities) => _entities.AddRange(entities);


        /// <summary>
        /// Lambda method for finding DbSet Entity record by Id and set its values with Entity passed as parameter.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void UpdateInDatabase(TEntity entity, int id) => _dbContext.Entry(_entities.Find(id)).CurrentValues.SetValues(entity);

        /// <summary>
        /// Lambda method for finding DbSet Entity record by Guid and set its values with Entity passed as parameter.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="guid"></param>
        public void UpdateInDatabase(TEntity entity, Guid guid) => _dbContext.Entry(_entities.Find(guid)).CurrentValues.SetValues(entity);


        /// <summary>
        /// Method for finding DbSet Entity record by Id,  
        /// call <see cref="AddToDatabase(TEntity entity)"/> if no Entity is fonud
        /// if Entity record is found set its values with Entity passed as parameter.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void AddOrUpdateInDatabase(TEntity entity, int id)
        {
            TEntity baseEntity = _entities.Find(id);
            if (baseEntity == null)
                AddToDatabase(entity);
            else
                _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// Method for finding DbSet Entity record by Guid,  
        /// call <see cref="AddToDatabase(TEntity entity)"/> if no Entity is fonud,
        /// if Entity record is found set its values with Entity passed as parameter.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="guid"></param>
        public void AddOrUpdateInDatabase(TEntity entity, Guid guid)
        {
            TEntity baseEntity = _entities.Find(guid);
            if (baseEntity == null)
                AddToDatabase(entity);
            else
                _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }


        /// <summary>
        /// Lambda method for finding DbSet Entity record by Guid and set it to Deleted state in DbContext.
        /// </summary>
        /// <param name="guid"></param>
        public void DeleteFromDatabase(Guid guid) => _entities.Remove(_entities.Find(guid));

        /// <summary>
        /// Lambda method for finding DbSet Entity record by Id set it to Deleted state in DbContext.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFromDatabase(int id) => _entities.Remove(_entities.Find(id));



        /// <summary>
        /// Lambda method for setting DbSet Entity to Deleted state in DbContext.
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteFromDatabase(TEntity entity) => _entities.Remove(entity);

        /// <summary>
        /// Lambda method for setting DbSet Entity collection to Deleted state in DbContext.
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRangeFromDatabase(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);


        /// <summary>
        /// Lambda method for setting DbSet Entity to Attached state.
        /// </summary>
        /// <param name="entity"></param>
        public void AttachEntity(TEntity entity) => _entities.Attach(entity);

        /// <summary>
        /// Lambda method for setting of DbSet Entity to state passed as EntityState parameter.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityState"></param>
        public void SetEntityState(TEntity entity, EntityState entityState) => _dbContext.Entry(entity).State = entityState;


        /// <summary>
        /// Lambda method for calling DbContext.SaveChanges()
        /// </summary>
        public void SaveChanges() => _dbContext.SaveChanges();

        /// <summary>
        /// Lambda method for async calling of DbContext.SaveChangesAsync()
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
