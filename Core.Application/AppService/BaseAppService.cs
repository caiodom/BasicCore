using AutoMapper;
using Core.Application.DTO;
using Core.DomainObjects;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.AppService
{
    public class BaseAppService<TSrc, TDest>:IBaseAppService<TSrc,TDest> where TSrc : BaseEntityDTO
                                             where TDest : BaseEntity, new()
    {

        private readonly IMapper _mapper;
        private readonly IBaseService<TDest> _baseService;
        public BaseAppService(IBaseService<TDest> baseService,
                              IMapper mapper)
        {
            this._baseService = baseService;
            this._mapper = mapper;
        }

        public virtual async Task<IEnumerable<TSrc>> GetAsync(bool asNoTracking = true)
                => GetMappedDTO(await _baseService.GetAsync(asNoTracking));



        public virtual async Task<TSrc> GetByIdAsync(Guid entityId, bool asNoTracking = true)
                => GetMappedDTO(await _baseService.GetByIdAsync(entityId, asNoTracking));



        public virtual async Task AddAsync(TSrc entity)
            => await _baseService.AddAsync(SetMappedDomainEntity(entity));



        public virtual async Task AddCollectionAsync(IEnumerable<TSrc> entities)
            =>  await _baseService.AddCollectionAsync(SetMappedDomainEntity(entities));

        public virtual async Task RemoveAsync(TSrc entity)
                => await _baseService.RemoveAsync(SetMappedDomainEntity(entity));

        public virtual async Task RemoveAsync(Guid id)
                => await _baseService.RemoveAsync(id);

        public virtual async Task UpdateAsync(TSrc entity)
                    => await  _baseService.UpdateAsync(SetMappedDomainEntity(entity));

        public virtual async Task UpdateCollectionAsync(IEnumerable<TSrc> entities)
                    => await _baseService.UpdateCollectionAsync(SetMappedDomainEntity(entities));

        protected TSrc GetMappedDTO(TDest domainEntity)
                => _mapper.Map<TSrc>(domainEntity);
        protected IEnumerable<TSrc> GetMappedDTO(IEnumerable<TDest> lstDomainEntity)
           => _mapper.Map<IEnumerable<TSrc>>(lstDomainEntity);

        protected TDest SetMappedDomainEntity(TSrc viewModelEntity)
                  => _mapper.Map<TDest>(viewModelEntity);
        protected IEnumerable<TDest> SetMappedDomainEntity(IEnumerable<TSrc> lstViewModelEntity)
                => _mapper.Map<IEnumerable<TDest>>(lstViewModelEntity);
    }
}


