using Core.DomainObjects;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        #region >> Variables <<
        private readonly IRepository<T> _baseRepository;
        #endregion

        #region >> Constructor <<
        public BaseService(IRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        #endregion

        #region >> Async <<

        public Task AddAsync(T entity) =>
                _baseRepository.AddAsync(entity);


        public Task AddCollectionAsync(IEnumerable<T> entities) =>
                    _baseRepository.AddCollectionAsync(entities);



        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                            => _baseRepository.GetAsync(expression, asNoTracking);


        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
                                   => _baseRepository.GetAsync(expression, orderBy, asNoTracking);

        public Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true)
                      => _baseRepository.GetByIdAsync(entityId, asNoTracking);

        public Task RemoveAsync(T entity)
                    => _baseRepository.RemoveAsync(entity);

        public Task RemoveByAsync(Func<T, bool> where)
               => _baseRepository.RemoveByAsync(where);


        public Task UpdateAsync(T entity)
                    => _baseRepository.UpdateAsync(entity);

        public Task UpdateCollectionAsync(IEnumerable<T> entities)
                    => _baseRepository.UpdateCollectionAsync(entities);

        #endregion

        #region >> Not Async <<
        public T GetById(Guid entityId, bool asNoTracking = true)
                    => _baseRepository.GetById(entityId, asNoTracking);

        public IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities)
                                => _baseRepository.UpdateCollectionWithProxy(entities);

        public IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities)
                           => _baseRepository.AddCollectionWithProxy(entities);

        public IEnumerable<T> Get(bool asNoTracking = true) =>
                                _baseRepository.Get(asNoTracking);

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                                => _baseRepository.Get(expression, asNoTracking);

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
                            => _baseRepository.Get(expression, orderBy, asNoTracking);

        public Task<IEnumerable<T>> GetAsync(bool asNoTracking = true)
                            => _baseRepository.GetAsync(asNoTracking);

        #endregion
    }
}
