using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using RLDatix.Weather.Application.Interfaces;
using RLDatix.Weather.Infrastructure;
using RLDatix.Weather.Infrastructure.Configuration;
using RLDatix.Weather.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Extensions.Http;
using RLDatix.Weather.Infrastructure.Constants;

namespace RLDatix.Weather.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastruture(this IServiceCollection services)
        {
            // Register IHttpClientFactory to DI container
            services.AddHttpClient("OpenWeatherMapApiClient").AddPolicyHandler(GetRetryPolicy());

            services.AddTransient<IExternalApiSerevice, OpenWeatherMapApiSerevice>();

            return services;
        }

        /// <summary>
        /// Below policy handle transient errors like network failures or http 5xx status codes 
        /// and retry 3 times with exponential backoff(2,4,8 secs) between retries.
        /// </summary>
        /// <returns></returns>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(Constants.RETRY_COUNT, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));
        }
    }
}
