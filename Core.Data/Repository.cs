
using Core.DomainObjects;
using Core.Interfaces;
using Core.Specification.Interfaces;
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
    public abstract class Repository<T>:IRepository<T> where T:BaseEntity,new()
    {
        protected MainContext Db;
        protected DbSet<T> DbSet;
        public IUnitOfWork UnitOfWork => Db;
        protected Repository(MainContext mainContext)
        {
            Db = mainContext;
            DbSet= Db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(bool asNoTracking = true)
                =>asNoTracking
                ? await DbSet.AsNoTracking()
                             .ToListAsync()
                : await DbSet.ToListAsync();
        

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression,bool asNoTracking = true)
                        => asNoTracking
                            ? await DbSet.AsNoTracking()
                                                .Where(expression)
                                                .ToListAsync()
                            : await DbSet.Where(expression).ToListAsync();


        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
                        => asNoTracking
                                ? await DbSet.AsNoTracking()
                                             .OrderBy(orderBy)
                                             .Where(expression)
                                              .ToListAsync()
                                : await DbSet.OrderBy(orderBy)
                                                .Where(expression)
                                                .ToListAsync();


        public async Task<IEnumerable<T>> GetAsync(ISpecification<T> spec, bool asNoTracking = true)
                                => await GetAsync(spec.IsSatisfiedBy(), asNoTracking);



        public virtual async Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true)
            => asNoTracking
                ? await DbSet.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == entityId).ConfigureAwait(false)
                : await DbSet.FindAsync(entityId).ConfigureAwait(false);
        

        public virtual async Task AddAsync(T entity)
                            =>await DbSet.AddAsync(entity);            
        

        public virtual async Task AddCollectionAsync(IEnumerable<T> entities)
                        => await DbSet.AddRangeAsync(entities);
            
        

        public virtual IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities)
        {
           

            foreach (var entity in entities)
            {
                DbSet.Add(entity);
                yield return entity;
            }
        }


        public virtual async Task UpdateAsync(T entity)
        {


            DbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task UpdateCollectionAsync(IEnumerable<T> entities)
        {
          
            DbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }


        public virtual IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities)
        {
          

            foreach (var entity in entities)
            {
                DbSet.Update(entity);
                yield return entity;
            }
        }

        public virtual async Task RemoveByAsync(Func<T, bool> where)
        {
          
            DbSet.RemoveRange(DbSet.Where(where));
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(T entity)
        {
          
            DbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            await RemoveAsync(entity);
        }

        public virtual async Task<bool> SaveChangesAsync()
                                =>await UnitOfWork.CommitAsync(); 
        



        public virtual IEnumerable<T> Get(bool asNoTracking = true)
        {
           
            if (asNoTracking)
            {
                return DbSet.AsNoTracking();
            }

            return DbSet;
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {

            if (asNoTracking)
            {
                return DbSet.AsNoTracking()
                                    .Where(expression);
            }

            return DbSet.Where(expression);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
        {
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

        public virtual bool  SaveChanges()
                        => UnitOfWork.Commit();
        
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
