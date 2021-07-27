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
    public class BaseController<TSrc, TDest> : MainController,IBaseController<TSrc> where TSrc : BaseEntityDTO
                                                                                    where TDest : BaseEntity
    {
        private readonly IBaseAppService<TSrc, TDest> _baseAppService;
        public BaseController(IBaseAppService<TSrc, TDest> baseAppService)
        {
            this._baseAppService = baseAppService;
        }


        //[ClaimsAuthorize(nameof(TDest), "GET")]
        [HttpGet]
        public virtual async Task<IEnumerable<TSrc>> Get()
                     => await _baseAppService.GetAsync();


        //[ClaimsAuthorize(nameof(TDest), "GET")]
        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TSrc>> GetById(Guid id)
        {
            var serviceReturn = await _baseAppService.GetByIdAsync(id);

            if (serviceReturn == null)
                return NotFound();

            return serviceReturn;
        }

        [ClaimsAuthorize(nameof(TDest), "POST")]
        [HttpPost]
        public virtual async Task<ActionResult<TSrc>> Post(TSrc entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService
                .AddAsync(entity);

            return CustomResponse(entity);
        }

        
        [ClaimsAuthorize(nameof(TDest), "PUT")]
        [HttpPut]
        public virtual async Task<ActionResult<TSrc>> Put(Guid id, TSrc entity)
        {

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService.UpdateAsync(entity);

            return CustomResponse(entity);
        }

        [ClaimsAuthorize(nameof(TDest), "DELETE")]
        [HttpDelete]
        public virtual async Task<ActionResult<TSrc>> Delete(TSrc entity)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _baseAppService.RemoveAsync(entity);

            return CustomResponse(entity);
        }

        protected virtual async Task<bool> UploadFileHandler(string currentDirectory, 
                                                                IFormFile file, 
                                                                string imgPrefix)
        {
            if (file == null || file.Length == 0)
            {
                AddProcessingErrors("Provide an image for this product");
                return false;
            }


            //currentDirectory=Directory.GetCurrentDirectory()
            var path = Path.Combine(currentDirectory, "wwwroot", imgPrefix, file.FileName);

            if (System.IO.File.Exists(path))
            {
                AddProcessingErrors("A file with this name already exists!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

    
    }
}
