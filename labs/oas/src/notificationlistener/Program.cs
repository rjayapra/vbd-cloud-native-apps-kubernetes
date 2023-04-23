using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationListener
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            await new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConfigurationRoot>(configuration);
                services.AddHttpContextAccessor();
                var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(x => { var result = !x.IsSuccessStatusCode; return result; })
                    .CircuitBreaker(3, TimeSpan.FromSeconds(60), OnBreak, OnReset, OnHalfOpen);
                services.AddSingleton<Logger>();
                services.AddSingleton<HttpClient>();
                services.AddSingleton<CircuitBreakerPolicy<HttpResponseMessage>>(circuitBreakerPolicy);
                services.AddSingleton<IResilientHttpClient, ResilientHttpClient>();
                services.AddSingleton<KafkaHttpClient>();
                services.AddSingleton<KafkaConsumer>();
                services.BuildServiceProvider();
                services.AddHostedService<NotificationHostedService>();
                services.AddApplicationInsightsTelemetryWorkerService();
            })
            .RunConsoleAsync();
            Console.ReadLine();
        }


        private static void OnHalfOpen() => throw new NotImplementedException();
        private static void OnReset() => throw new NotImplementedException();
        private static void OnBreak(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2) => throw new NotImplementedException();


    }
}
