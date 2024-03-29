﻿using Core.DomainObjects;
using Core.Specification.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T>  where T : BaseEntity, new()
    {

        Task<IEnumerable<T>> GetAsync(bool asNoTracking = true);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);


        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true);

        Task<IEnumerable<T>> GetAsync(ISpecification<T> spec,bool asNoTracking=true);


        Task<T> GetUniqueAsync(ISpecification<T> spec,bool asNoTracking=true,
                                                    bool isFirst=false,
                                                      bool isSingle=false);
        Task<T> GetUniqueAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true,
                                                                    bool isFirst = false,
                                                                    bool isSingle = false);

        Task<T> GetByIdAsync(Guid entityId, bool asNoTracking = true);

        Task<List<T>> GetDataAsync(Expression<Func<T, bool>> expression = null,
                                   Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    int? skip = null,
                                    int? take = null);

        Task AddAsync(T entity);


        Task AddCollectionAsync(IEnumerable<T> entities);


        IEnumerable<T> AddCollectionWithProxy(IEnumerable<T> entities);



        Task UpdateAsync(T entity);


        Task UpdateCollectionAsync(IEnumerable<T> entities);


        IEnumerable<T> UpdateCollectionWithProxy(IEnumerable<T> entities);


        Task RemoveByAsync(Func<T, bool> where);

        void Remove(Guid id);
        Task RemoveAsync(T entity);
        Task RemoveAsync(Guid id);

        Task<bool> ConditionalQueryAsync(ISpecification<T> spec, bool asNoTracking = true);
        Task<bool> ConditionalQueryAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);

        IQueryable<T> ValidateTracking(bool asNoTracking);

        IEnumerable<T> Get(bool asNoTracking = true);


        IEnumerable<T> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true);


        IEnumerable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy, bool asNoTracking = true);


        T GetById(Guid entityId, bool asNoTracking = true);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        void DettachMe(T entity);
    }
}
