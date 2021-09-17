using Core.Messages;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mediator
{
    public interface IMediatorHandler
    {
        public Task PublishEvent<T>(T eventEntity) where T : Event;

        Task<ValidationResult> SendCommand<T>(T command) where T : Command;

    }
}
