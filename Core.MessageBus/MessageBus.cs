using Core.Messages.Integration;
using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessageBus
{
    public class MessageBus : IMessageBus
    {
        protected IBus _bus;
        protected IAdvancedBus _advancedBus;



        private readonly string _connectionString;
        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }
        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        #region >> Private Methods <<

        protected void TryConnect()
        {
            if (IsConnected)
                return;

            var policy = Policy.Handle<EasyNetQException>()
                             .Or<BrokerUnreachableException>()
                             .WaitAndRetry(3, retryAttempt =>
                                             TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
            });

            _advancedBus = _bus.Advanced;
            /*
               If the server goes down and the app realizes that it no longer has a connection, we will add the
                EventHandler pointing to OnDisconnect
            */
            _advancedBus.Disconnected += OnDisconnect;

        }


        protected void OnDisconnect(object sender, EventArgs e)
        {
            /*
                We put Polly which is a circuitBreaker trying to reconnect all the time
                with the RetryForever method
            */
            var policy = Policy.Handle<EasyNetQException>()
                             .Or<BrokerUnreachableException>()
                             .RetryForever();

            policy.Execute(TryConnect);
        }
        #endregion



        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();

            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();

            await _bus.PubSub.PublishAsync(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public async Task SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            await _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }


        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {

            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);

        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Respond(responder);
        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.RespondAsync<TRequest, TResponse>(responder)
                           .GetAwaiter()
                           .GetResult();

        }


        public void Dispose()
        {
            _bus.Dispose();
        }
    }


    public class TypedMessageBus<T> : MessageBus, ITypedMessageBus<T> where T : class, new()
    {
        public TypedMessageBus(string connectionString) : base(connectionString)
        {

        }

        public  TResponse TypedRequest<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : TypedResponseMessage<T>
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> TypedRequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : TypedResponseMessage<T>
        {
            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable TypedRespond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : TypedResponseMessage<T>
        {
            base.TryConnect();
            return _bus.Rpc.Respond(responder);
        }

        public IDisposable TypedRespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : TypedResponseMessage<T>
        {
            TryConnect();
            return _bus.Rpc.RespondAsync<TRequest, TResponse>(responder)
                           .GetAwaiter()
                           .GetResult();
        }
    }
}
