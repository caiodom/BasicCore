using Core.DomainObjects;
using Core.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity, new()
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<T>> GetAsync(bool asNoTracking = true);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);


        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true);

        Task<IEnumerable<T>> GetAsync(ISpecification<T> spec,bool asNoTracking=true);

        Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true);

        Task AddAsync(T entity);


        Task AddCollectionAsync(IEnumerable<T> entities);


        IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities);



        Task UpdateAsync(T entity);


        Task UpdateCollectionAsync(IEnumerable<T> entities);


        IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities);


        Task RemoveByAsync(Func<T, bool> where);


        Task RemoveAsync(T entity);


        Task<bool> SaveChangesAsync();

        bool SaveChanges();

        IEnumerable<T> Get(bool asNoTracking = true);


        IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true);


        IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true);


        T GetById(Guid entityId, bool asNoTracking = true);
    }
}
