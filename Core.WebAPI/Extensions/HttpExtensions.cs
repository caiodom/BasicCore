using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Extensions
{
    public static class HttpExtensions
    {

        public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
        {
            if (builder == null)
                throw new ArgumentException(nameof(builder));

            return builder.ConfigureHttpMessageHandlerBuilder(builder => {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = 
                                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            
            });
        }
    }
}
