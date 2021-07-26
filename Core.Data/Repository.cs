using Core.Data.Interfaces;
using Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class Repository<T>:IRepository<T> where T:BaseEntity,new()
    {
        protected MainContext Db;
        protected DbSet<T> DbSet;
        public IUnitOfWork UnitOfWork => Db;
        public Repository()
        {
            
        }

        public void SetDbContext(MainContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            Db = dbContext;
            DbSet = Db.Set<T>();
            
        }
        public virtual async Task<IEnumerable<T>> GetAsync(bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");


            if (asNoTracking)
            {
                return await DbSet.AsNoTracking()
                                    .ToListAsync();
            }

            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            if (asNoTracking)
            {
                return await DbSet.AsNoTracking()
                                    .Where(expression)
                                    .ToListAsync();
            }

            return await DbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            if (asNoTracking)
                return await DbSet.AsNoTracking().OrderBy(orderBy).Where(expression).ToListAsync().ConfigureAwait(false);



            return await DbSet.OrderBy(orderBy)
                                .Where(expression)
                                .ToListAsync()
                                .ConfigureAwait(false);

        }

        public virtual async Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            return asNoTracking
                ? await DbSet.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == entityId).ConfigureAwait(false)
                : await DbSet.FindAsync(entityId).ConfigureAwait(false);
        }

        public virtual async Task AddAsync(T entity)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            DbSet.Add(entity);
            ///await DbSet.AddAsync(entity).ConfigureAwait(false);
            await SaveChangesAsync();
        }

        public virtual async Task AddCollectionAsync(IEnumerable<T> entities)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            DbSet.AddRange(entities);
            await SaveChangesAsync();
        }

        public virtual IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            foreach (var entity in entities)
            {
                DbSet.Add(entity);
                yield return entity;
            }
        }


        public virtual Task UpdateAsync(T entity)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateCollectionAsync(IEnumerable<T> entities)
        {
            if (Db == null)
                throw new Exception("Context not found!!"); 

            DbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }


        public virtual IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            foreach (var entity in entities)
            {
                DbSet.Update(entity);
                yield return entity;
            }
        }

        public virtual Task RemoveByAsync(Func<T, bool> where)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            DbSet.RemoveRange(DbSet.ToList().Where(where));
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(T entity)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync()
        {
            if (Db == null)
                throw new Exception("Context not found!!"); 

            await Db.SaveChangesAsync().ConfigureAwait(false);
        }



        public virtual IEnumerable<T> Get(bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            if (asNoTracking)
            {
                return DbSet.AsNoTracking();
            }

            return DbSet;
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            if (asNoTracking)
            {
                return DbSet.AsNoTracking()
                                    .Where(expression);
            }

            return DbSet.Where(expression);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");


            if (asNoTracking)
                return DbSet.AsNoTracking().OrderBy(orderBy).Where(expression);



            return DbSet.OrderBy(orderBy)
                                .Where(expression);

        }

        public virtual T GetById(Guid entityId, bool asNoTracking = true)
        {
            if (Db == null)
                throw new Exception("Context not found!!");

            return asNoTracking
                ? DbSet.AsNoTracking().SingleOrDefault(entity => entity.Id == entityId)
                : DbSet.Find(entityId);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
