using CQRS.Application.Configuration.Commands;
using CQRS.Domain.Customers;
using MediatR;
using Newtonsoft.Json;
using System;

namespace CQRS.Application.Customers
{
    public class MarkCustomerAsWelcomedCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : base(id)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}