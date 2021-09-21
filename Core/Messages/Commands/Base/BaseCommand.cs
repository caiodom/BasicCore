using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages.Commands.Base
{
    public abstract class BaseCommand : Message
    {

        public DateTime Timestamp { get; set; }

        public ValidationResult ValidationResult { get; set; }

        protected BaseCommand(DateTime dtTimeStamp)
        {
            Timestamp = dtTimeStamp;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
