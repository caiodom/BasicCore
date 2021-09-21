using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessageBus.Extensions
{
   public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services,string connection)
        {
            if (string.IsNullOrEmpty(connection))
                throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }

        public static IServiceCollection AddTypedMessageBus<T>(this IServiceCollection services, string connection) where T:class,new ()
        {
            if (string.IsNullOrEmpty(connection))
                throw new ArgumentNullException();


            services.AddSingleton<ITypedMessageBus<T>>(serviceProvider => new TypedMessageBus<T>(connection));

            return services;
        }
    }
}
