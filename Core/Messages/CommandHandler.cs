using Core.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;
        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }


        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {

            try
            {


                if (!await uow.SaveChangesAsync())
                    AddError("There was an error persisting the data.");
                

                 uow.Commit();


                return ValidationResult;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
