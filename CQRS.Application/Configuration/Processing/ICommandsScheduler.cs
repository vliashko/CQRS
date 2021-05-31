using CQRS.Application.Configuration.Commands;
using System.Threading.Tasks;

namespace CQRS.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}