using CQRS.Application.Configuration.Commands;
using CQRS.Infrastructure.Processing.Outbox;
using MediatR;

namespace CQRS.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}