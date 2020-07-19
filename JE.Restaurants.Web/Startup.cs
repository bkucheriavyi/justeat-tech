using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using JE.Restaurant.WebApi.Services.Interfaces;
using JE.Restaurant.WebApi.Middlewares;
using JE.Restaurant.WebApi.Services;
using JE.JustEat.Public.Client.DI;

namespace JE.Restaurant.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddAutoMapper(o =>
            {
                o.AllowNullCollections = false;
            }, AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));

            services.AddJustEatClientClient(c => c.ServiceUrl = Configuration["JustEat:ServiceUrl"]);

            services.AddTransient<IRestaurantService, RestaurantService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
