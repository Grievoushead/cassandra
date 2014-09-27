using System.Threading.Tasks;

namespace Contract
{
    public interface ICommand
    {
        bool IsAsync { get; }
        string Run();
        Task<string> RunAsync();
    }
}