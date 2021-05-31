using CQRS.Application.Orders;
using System.Collections.Generic;

namespace CQRS.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<ProductDto> Products { get; set; }

        public string Currency { get; set; }
    }
}