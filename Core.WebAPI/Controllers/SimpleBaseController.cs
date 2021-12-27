using Core.DomainObjects;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Controllers
{
    public abstract class SimpleBaseController<T>:MainController where T:BaseEntity, new()
    {
        private readonly IBaseService<T> _baseService;

        public SimpleBaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<T>> Get()
                   => await _baseService.GetAsync();



        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetById(Guid id)
        {
            var serviceReturn = await _baseService.GetByIdAsync(id);

            if (serviceReturn == null)
                return NotFound();

            return CustomResponse(serviceReturn);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post(T entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseService
                .AddAsync(entity);

            return CustomResponse(entity);
        }



        [HttpPut]
        public virtual async Task<ActionResult> Put(Guid id, T entity)
        {

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseService.UpdateAsync(entity);

            return CustomResponse(entity);
        }


        [HttpDelete]
        public virtual async Task<ActionResult> Delete(T entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseService.RemoveAsync(entity);

            return CustomResponse(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseService.RemoveAsync(id);

            return CustomResponse();
        }
    }
}
