using CQRS.Application.Configuration.Commands;
using MediatR;

namespace CQRS.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}