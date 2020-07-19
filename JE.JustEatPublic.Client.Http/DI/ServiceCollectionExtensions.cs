using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using JE.JustEat.Public.Client.Http.Configuration;

namespace JE.JustEat.Public.Client.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJustEatClientClient(this IServiceCollection services, Action<JustEatPublicApiClientConfig> configCallback)
        {
            var config = new JustEatPublicApiClientConfig();
            configCallback?.Invoke(config);
            AddJustEatClientClient(services, config);
            return services;
        }

        public static IServiceCollection AddJustEatClientClient(this IServiceCollection services, JustEatPublicApiClientConfig config)
        {
            services.AddSingleton<JustEatPublicApiClientConfig>(c => new JustEatPublicApiClientConfig
            {
                ServiceUrl = config.ServiceUrl
            });

            services.AddHttpClient<IJustEatPublicApiClient, JustEatPublicApiClient>(config.ServiceUrl)
                    .ConfigureHttpClient((sp, client) =>
                    {
                        client.BaseAddress = new Uri(sp.GetService<JustEatPublicApiClientConfig>().ServiceUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    });

            services.AddScoped<IRestaurantResource, RestaurantResource>();
            services.AddScoped<IJustEatPublicApiClient, JustEatPublicApiClient>();

            return services;
        }
    }
}
