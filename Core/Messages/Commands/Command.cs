using Core.Messages.Commands.Base;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
   public  class Command: BaseCommand, IRequest<ValidationResult>
    {
        

        protected Command():base(DateTime.Now)
        {
            
        }

    }
}
