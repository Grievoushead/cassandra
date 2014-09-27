using System.Threading.Tasks;

namespace Contract
{
    public interface ICommandContext
    {
        Task<string> Execute(string cmd);
    }
}