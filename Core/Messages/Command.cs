﻿using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
   public abstract class Command:Message,IRequest<ValidationResult>
    {

        public DateTime Timestamp { get; set; }

        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }


        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
