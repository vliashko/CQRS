using System;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
