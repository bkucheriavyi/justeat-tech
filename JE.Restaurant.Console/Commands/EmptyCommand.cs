using System.Threading.Tasks;

namespace JE.Restaurant.Console
{
    public class EmptyCommand : IConsoleCommand
    {
        public Task ExecuteAsync(string[] args)
        {
            System.Console.WriteLine("Nothing to do...");

            return Task.CompletedTask;
        }
    }
}
