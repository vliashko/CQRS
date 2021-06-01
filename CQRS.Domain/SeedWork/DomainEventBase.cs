using System;

namespace CQRS.Domain.SeedWork
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccurredOn = DateTime.UtcNow;
        }

        public DateTime OccurredOn { get; }
    }
}