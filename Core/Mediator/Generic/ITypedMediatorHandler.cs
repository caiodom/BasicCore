using Core.Messages;
using Core.Messages.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mediator.Generic
{
    public interface ITypedMediatorHandler<TEntity> where TEntity:class
    {
        public Task PublishEvent<T>(T eventEntity) where T : Event;

        Task<TEntity> SendCommand<T>(T command) where T : TypedCommand<TEntity>;

    }

    public interface ITypedMediatorHandlerCollection<TEntity> where TEntity : class
    {

        public Task PublishEvent<T>(T eventEntity) where T : Event;
        Task<IEnumerable<TEntity>> SendCommand<T>(T command) where T : TypedCollectionCommand<TEntity>;

    }


}
