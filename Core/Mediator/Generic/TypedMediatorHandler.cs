using Core.Messages;
using Core.Messages.Integration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Mediator.Generic
{
    public class TypedMediatorHandler<TEntity> : ITypedMediatorHandler<TEntity> where TEntity:class,new()
    {

        private readonly IMediator _mediator;

        public TypedMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task PublishEvent<T>(T eventEntity) where T : Event
        {
            await _mediator.Publish(eventEntity);
        }

        public async Task<TEntity> SendCommand<T>(T command) where T : TypedCommand<TEntity>
        {
            return await _mediator.Send(command);
        }


       
    }


    public class TypedMediatorHandlerCollection<TEntity> : ITypedMediatorHandlerCollection<TEntity> where TEntity : class, new()
    {

        private readonly IMediator _mediator;

        public TypedMediatorHandlerCollection(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T eventEntity) where T : Event
        {
            await _mediator.Publish(eventEntity);
        }

        public async Task<IEnumerable<TEntity>> SendCommand<T>(T command) where T : TypedCollectionCommand<TEntity>
        {
            return await _mediator.Send(command);
        }
    }
}
