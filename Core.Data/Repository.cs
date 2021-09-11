
using Core.Data.Extensions;
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
    public class Repository<T>:IRepository<T> where T:BaseEntity,new()
    {
        
        protected readonly DbSet<T> DbSet;
        public Repository(MainContext mainContext)
        {
            DbSet = mainContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(bool asNoTracking = true)
                =>asNoTracking
                ? await DbSet.AsNoTracking()
                             .ToListAsync()
                : await DbSet.ToListAsync();


        public virtual async Task<bool> ConditionalQueryAsync(ISpecification<T> spec, bool asNoTracking = true)
                         => asNoTracking
                            ? await DbSet.AsNoTracking().AnyAsync(spec.IsSatisfiedBy())
                            : await DbSet.AnyAsync(spec.IsSatisfiedBy());
                
                   
        public virtual async Task<bool> ConditionalQueryAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                         => asNoTracking
                            ? await DbSet.AsNoTracking().AnyAsync(expression)
                            : await DbSet.AnyAsync(expression);



        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression,bool asNoTracking = true)
                        => asNoTracking
                            ? await DbSet.AsNoTracking()
                                                .Where(expression)
                                                .ToListAsync()
                            : await DbSet.Where(expression).ToListAsync();


        public Task<T> GetUniqueAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true, bool isFirst = false, bool isSingle = false)
        {
            IQueryable<T> queryable = ValidateTracking(asNoTracking);

            if (isFirst.Equals(isSingle))
                throw new Exception($"{nameof(isFirst)} parameter cannot be the same as { nameof(isSingle)}");


            if (isSingle)
                return queryable.SingleOrDefaultAsync(expression);
            else
                return queryable.FirstOrDefaultAsync(expression);

        }
        public Task<T> GetUniqueAsync(ISpecification<T> spec, bool asNoTracking = true, bool isFirst = false, bool isSingle = false)
        {
            IQueryable<T> queryable= ValidateTracking(asNoTracking);

            if (isFirst.Equals(isSingle))
                throw new Exception($"{nameof(isFirst)} parameter cannot be the same as { nameof(isSingle)}");


            if (isSingle)
                return queryable.SingleOrDefaultAsync(spec.IsSatisfiedBy());
            else 
                return queryable.FirstOrDefaultAsync(spec.IsSatisfiedBy());

        }

        

        public IQueryable<T> ValidateTracking(bool asNoTracking)
                => (asNoTracking)
                        ? DbSet.AsNoTracking()
                        : DbSet;

        public Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true, bool isFirst = false, bool isSingle = false)
        {
            throw new NotImplementedException();
        }


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
            return asNoTracking
                ? DbSet.AsNoTracking().SingleOrDefault(entity => entity.Id == entityId)
                : DbSet.Find(entityId);
        }

       
        

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }
        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }


       
    }
}
