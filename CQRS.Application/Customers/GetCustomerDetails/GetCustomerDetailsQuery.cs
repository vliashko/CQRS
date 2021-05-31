using CQRS.Application.Configuration.Queries;
using System;

namespace CQRS.Application.Customers.GetCustomerDetails
{
    public class GetCustomerDetailsQuery : IQuery<CustomerDetailsDto>
    {
        public GetCustomerDetailsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}