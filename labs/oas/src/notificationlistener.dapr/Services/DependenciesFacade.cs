using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace notificationlistener.dapr.Services
{
    public class DependenciesFacade
    {

        private static DependenciesFacade _instance;

        public static DependenciesFacade Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DependenciesFacade();
                return _instance;
            }
        }

        public void RegisterDependenciesAndHookupHostedServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(x => { var result = !x.IsSuccessStatusCode; return result; })
                .CircuitBreaker(3, TimeSpan.FromSeconds(60), OnBreak, OnReset, OnHalfOpen);
            services.AddSingleton<Logger>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<CircuitBreakerPolicy<HttpResponseMessage>>(circuitBreakerPolicy);
            services.AddSingleton<IResilientHttpClient, ResilientHttpClient>();
            services.AddSingleton<KafkaHttpClient>();
        }

        private void OnHalfOpen() => throw new NotImplementedException();
        private void OnReset() => throw new NotImplementedException();
        private void OnBreak(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2) => throw new NotImplementedException();
    }
}