using Core.Application.DTO;
using Core.DomainObjects;
using Core.Interfaces;
using Core.WebAPI.Identity.Attribute;
using Core.WebAPI.User.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;

namespace Core.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TSrc, TDest> : MainController,IBaseController<TSrc> where TSrc : BaseEntityDTO
                                                                                              where TDest : BaseEntity
    {
        private readonly IBaseAppService<TSrc, TDest> _baseAppService;
        protected  BaseController(IBaseAppService<TSrc, TDest> baseAppService)
        {
            this._baseAppService = baseAppService;
        }


        
        [HttpGet]
        public  virtual async Task<IEnumerable<TSrc>> Get()
                     => await _baseAppService.GetAsync();



        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetById(Guid id)
        {
            var serviceReturn = await _baseAppService.GetByIdAsync(id);

            if (serviceReturn == null)
                return NotFound();

            return CustomResponse(serviceReturn);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post(TSrc entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService
                .AddAsync(entity);

            return CustomResponse(entity);
        }

        

        [HttpPut]
        public virtual async Task<ActionResult> Put(Guid id, TSrc entity)
        {

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService.UpdateAsync(entity);

            return CustomResponse(entity);
        }


        [HttpDelete]
        public virtual async Task<ActionResult> Delete(TSrc entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService.RemoveAsync(entity);

            return CustomResponse(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService.RemoveAsync(id);

            return CustomResponse();
        }

      

    
    }
}
