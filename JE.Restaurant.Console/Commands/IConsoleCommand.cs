using System.Threading.Tasks;

namespace JE.Restaurant.Console
{
    public interface IConsoleCommand
    {
        Task ExecuteAsync(string[] args);
    }
}
