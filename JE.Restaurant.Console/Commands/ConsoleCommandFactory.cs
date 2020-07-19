using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JE.Restaurant.Console
{
    public static class ConsoleCommandFactory
    {
        public static IConsoleCommand Create(string[] args)
        {
            if (args.Contains("--get-restaurants"))
            {
                return new RunGetRestaurantByPostCodeCommand();
            }

            return new EmptyCommand();
        }

        private static Lazy<(string Environment, IConfiguration AppConfig)> Settings { get; } = new Lazy<(string, IConfiguration)>(BuildSettings);

        private static Lazy<IServiceProvider> ServiceProvider { get; } = new Lazy<IServiceProvider>(BuildServiceProvider(Settings.Value.AppConfig));

        private static (string, IConfiguration) BuildSettings()
        {
            var envValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var env = !string.IsNullOrEmpty(envValue) ? envValue : "Local";
            var builder = new ConfigurationBuilder()
                          .AddJsonFile($"appsettings.json", true, true)
                          .AddJsonFile($"appsettings.{env}.json", true, true)
                          .AddEnvironmentVariables();

            return (env, builder.Build());
        }

        private static IServiceProvider BuildServiceProvider(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            return services.BuildServiceProvider();
        }
    }
}
