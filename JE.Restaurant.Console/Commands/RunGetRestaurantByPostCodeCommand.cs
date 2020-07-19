using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

namespace JE.Restaurant.Console
{
    public class RunGetRestaurantByPostCodeCommand : IConsoleCommand
    {
        private static string WebHostUrl = "http://localhost:1234";

        public async Task ExecuteAsync(string[] args)
        {
            ThrowIfMissingRequredArgs(args);
            var ct = new CancellationTokenSource();

            await RunUsingLocalWebServer(ct, async ctx =>
            {
                var postCode = args[2];
                var result = await GetRestaurants(ctx.WebHostUrl, postCode);
                if (result == string.Empty)
                {
                    System.Console.WriteLine($"Sorry, nothing was found for {postCode}");
                }
                else
                {
                    System.Console.WriteLine($"Listing results:");
                    TryPrintResults(result);
                }
            });

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }

        private void TryPrintResults(string jsonString)
        {
            try
            {
                
                JArray jsonVal = JArray.Parse(jsonString) as JArray;
                dynamic restaurants = jsonVal;
                System.Console.WriteLine($"Found restaurants in the area: {jsonVal.Count}");
                foreach (dynamic res in restaurants)
                {
                    System.Console.WriteLine($"[{res.name}], Rating: { res.rating}, Food: {string.Join(",", res.foodTypes)}");
                }
            }
            catch
            {
                System.Console.WriteLine("Sorry, I was unable to parse results.Error response?");
            }
            
        }

        private class CurrentContext
        {
            public string WebHostUrl { get; set; }
        }

        private async static Task RunUsingLocalWebServer(CancellationTokenSource ct, Func<CurrentContext, Task> action)
        {
            var hostingTask = Task.Run(async () =>
            {
                try
                {
                    await JE.Restaurant.WebApi.Program
                     .CreateHostBuilder(new[] { "--hosting-url", WebHostUrl })
                     .RunConsoleAsync(ct.Token);
                }
                catch (TaskCanceledException) { }
            });

            try
            {
                await action(new CurrentContext { WebHostUrl = WebHostUrl });
            }
            finally
            {
                System.Console.WriteLine("Shutting down web server...");
                ct.Cancel();
                await hostingTask;
            }
        }

        private static void ThrowIfMissingRequredArgs(string[] args)
        {
            if (args.Length < 3 ||
                            (args[1].ToLowerInvariant() != "-code" &&
                            args[1].ToLowerInvariant() != "-c"))
            {
                throw new ArgumentException($"{nameof(RunGetRestaurantByPostCodeCommand)} requires to have post code specified through the argument (-code || -c), e.g [-c ec4m]");
            }
        }

        private async Task<string> GetRestaurants(string webHostUrl, string postCode)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(webHostUrl)
            };

            using (httpClient)
            {
                int attempt = 0;
                string result = string.Empty;
                while (attempt < 3)
                {
                    HttpResponseMessage response = null;
                    try
                    {
                        response = await httpClient.GetAsync($"{webHostUrl}/restaurants/byPostCode/{postCode}");
                    }
                    catch (HttpRequestException)
                    {
                        System.Console.WriteLine($"No luck, maybe web service is yet started?\nLet's try again...({attempt + 1}/{3})");
                        await Task.Delay(TimeSpan.FromSeconds(5));
                        attempt++;
                        continue;
                    }

                    System.Console.WriteLine("Got some response, reading as string...");
                    result = await response.Content.ReadAsStringAsync();
                    break;
                }

                return result;
            }
        }
    }
}
