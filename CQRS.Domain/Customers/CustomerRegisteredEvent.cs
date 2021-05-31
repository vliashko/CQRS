using CQRS.Domain.SeedWork;

namespace CQRS.Domain.Customers
{
    public class CustomerRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public CustomerRegisteredEvent(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}