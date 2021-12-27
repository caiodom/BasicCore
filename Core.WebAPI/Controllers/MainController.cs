using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Controllers
{

    [ApiController]
    public abstract class MainController:Controller
    {

        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages",Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
                AddProcessingErrors(error.ErrorMessage);

            return CustomResponse();
        }

        protected void AddProcessingErrors(string error)
        {
            Errors.Add(error);
        }

        protected bool ValidOperation()
                    => !Errors.Any();

        protected void CleanProcessingErrors()
        {
            Errors.Clear();
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
