using Core.DomainObjects;
using Core.Interfaces;
using Core.Specification.Interfaces;
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

        public virtual async Task AddAsync(T entity) 
            => await _baseRepository.AddAsync(entity);


        public virtual async Task AddCollectionAsync(IEnumerable<T> entities)
                     => await _baseRepository.AddCollectionAsync(entities);



        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                            => await _baseRepository.GetAsync(expression, asNoTracking);


        public virtual async  Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
                                   => await _baseRepository.GetAsync(expression, orderBy, asNoTracking);

        public virtual async Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true)
                      => await _baseRepository.GetByIdAsync(entityId, asNoTracking);

        public virtual async Task<IEnumerable<T>> GetAsync(bool asNoTracking = true)
                    => await _baseRepository.GetAsync(asNoTracking);

        public virtual async  Task RemoveAsync(T entity)
                    => await _baseRepository.RemoveAsync(entity);
        public virtual async Task RemoveAsync(Guid id)
            => await _baseRepository.RemoveAsync(id);



        public virtual async Task RemoveByAsync(Func<T, bool> where)
               => await _baseRepository.RemoveByAsync(where);


        public virtual async Task UpdateAsync(T entity)
                    => await _baseRepository.UpdateAsync(entity);

        public virtual async Task UpdateCollectionAsync(IEnumerable<T> entities)
                    => await _baseRepository.UpdateCollectionAsync(entities);

        public async Task<bool> ConditionalQueryAsync(ISpecification<T> spec, bool asNoTracking = true)
                    => await _baseRepository.ConditionalQueryAsync(spec,asNoTracking);

        public async Task<bool> ConditionalQueryAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                        => await _baseRepository.ConditionalQueryAsync(expression, asNoTracking);

        public async Task<T> GetUniqueAsync(ISpecification<T> spec,
                                                    bool asNoTracking = true,
                                                    bool isFirst = false, bool
                                                    isSingle = false)
                                                        => await _baseRepository
                                                                    .GetUniqueAsync(spec, asNoTracking, isFirst, isSingle);

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression,
                                      bool asNoTracking = true,
                                      bool isFirst = false,
                                      bool isSingle = false)
                                                 => await _baseRepository
                                                            .GetUniqueAsync(expression, asNoTracking, isFirst, isSingle);



        #endregion

        #region >> Not Async <<
        public virtual T GetById(Guid entityId, bool asNoTracking = true)
                    => _baseRepository.GetById(entityId, asNoTracking);

        public virtual IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities)
                                => _baseRepository.UpdateCollectionWithProxy(entities);

        public virtual IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities)
                           => _baseRepository.AddCollectionWithProxy(entities);

        public virtual IEnumerable<T> Get(bool asNoTracking = true) =>
                                _baseRepository.Get(asNoTracking);

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true)
                                => _baseRepository.Get(expression, asNoTracking);

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true)
                            => _baseRepository.Get(expression, orderBy, asNoTracking);

        public virtual void Add(T entity)
                    => _baseRepository.Add(entity);

        public virtual async Task<IEnumerable<T>> GetAsync(ISpecification<T> spec, bool asNoTracking = true)
                => await _baseRepository.GetAsync(spec, true);

        public virtual void Update(T entity) 
            => _baseRepository.Update(entity);

        public virtual void Remove(T entity)
            => _baseRepository.Remove(entity);

        public virtual void DettachMe(T entity)
            => _baseRepository.DettachMe(entity);



        #endregion
    }
}
