using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
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
    }
}
