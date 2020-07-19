using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace JE.Restaurant.WebApi.Tests.Component
{
    public static class TestServerFactory
    {
        public static IHttpClientFactory _clientFactory;
        public static WebApplicationFactory<Startup> CreateTestServerFactory(Action<IServiceCollection> registerServices)
        {
            return new WebApplicationFactory<Startup>().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(typeof(IHostedService));
                        registerServices(services);
                    });
                });
        }
    }
}
