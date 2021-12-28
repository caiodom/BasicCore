using Core.Application.DTO;
using Core.WebAPI.BffServices.Interfaces;
using Core.WebAPI.User.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Controllers
{
    public class BaseBFFController<T>:MainController where T: BaseEntityDTO
    {

        private readonly IBaseBFFService<T> _baseService;
        private readonly IUser _appUser;
        public BaseBFFController(IBaseBFFService<T> baseService, IUser appUser)
        {
            _baseService = baseService;
            _appUser = appUser;
        }

        [HttpGet]
        public virtual async Task<ActionResult> Get()
                => CustomResponse(await _baseService.Get());


        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetById(Guid id)
            => CustomResponse(await _baseService.GetById(id));


        [HttpPost]
        public virtual async Task<ActionResult> Post([FromBody] T entity)
        {
            try
            {
                return CustomResponse(await _baseService.PostAsync(entity));
            }
            catch (Exception ex)
            {
                AddProcessingErrors("Errors: " + ex.Message);
                return CustomResponse(BadRequest());
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] T entity)
        {
            try
            {
                return CustomResponse(await _baseService.PutAsync(id, entity));
            }
            catch (Exception ex)
            {
                AddProcessingErrors("Errors: " + ex.Message);
                return CustomResponse(BadRequest());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _baseService.DeleteAsync(id);
                return CustomResponse(Ok());

            }
            catch (Exception ex)
            {
                AddProcessingErrors("Errors: " + ex.Message);
                return CustomResponse(BadRequest());
            }

        }



        protected Guid GetUser()
                 => _appUser.GetUserId();

 


    }
}
