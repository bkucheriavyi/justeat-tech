using System;
using System.Threading.Tasks;

namespace JE.Restaurant.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Starting command execution...");

            try
            {
                var command = ConsoleCommandFactory.Create(args);
                await command.ExecuteAsync(args);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

            System.Console.WriteLine("Shutdown");
        }
    }
}
