using System.Threading.Tasks;
using Contract;
using DI;
using Microsoft.Practices.Unity;

namespace Service
{
    public class CommandContext : ICommandContext
    {
        public async Task<string> Execute(string cmd)
        {
            try
            {
                var command = IoC.Resolve<ICommand>(cmd.ToLower());
                if (command.IsAsync)
                {
                    return await command.RunAsync();
                }

                return await Task.FromResult(command.Run());
            }
            catch (ResolutionFailedException ex)
            {
                return "I dunno(";
            }
        }
    }
}
