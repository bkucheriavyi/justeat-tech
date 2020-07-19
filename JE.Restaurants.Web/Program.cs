using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace JE.Restaurant.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

          Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
            {
                if (args.Length > 0 && args[0] == "--hosting-url")
                {
                    webBuilder.UseUrls(args[1]);
                }
                webBuilder.UseStartup<Startup>();
            });
    }
}
