using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Extensions
{
    public static class PollyExtensions
    {

        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetry()
            => HttpPolicyExtensions.HandleTransientHttpError()
                                  .WaitAndRetryAsync(new[] {
                                              TimeSpan.FromSeconds(1),
                                              TimeSpan.FromSeconds(5),
                                              TimeSpan.FromSeconds(10),
                                  }, (outCome, retryCount, context) =>
                                  {
                                      Console.ForegroundColor = ConsoleColor.Blue;
                                      Console.WriteLine($"Trying for the {retryCount} time!");
                                      Console.ForegroundColor = ConsoleColor.White;
                                  });
    }
}
