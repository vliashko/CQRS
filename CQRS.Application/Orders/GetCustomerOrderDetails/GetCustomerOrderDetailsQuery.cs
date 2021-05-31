using CQRS.Application.Configuration.Queries;
using System;

namespace CQRS.Application.Orders.GetCustomerOrderDetails
{
    public class GetCustomerOrderDetailsQuery : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; }

        public GetCustomerOrderDetailsQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}