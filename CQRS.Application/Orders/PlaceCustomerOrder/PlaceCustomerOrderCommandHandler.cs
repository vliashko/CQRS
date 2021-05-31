using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Data;
using CQRS.Domain.Customers;
using CQRS.Domain.Customers.Orders;
using CQRS.Domain.ForeignExchange;
using CQRS.Domain.Products;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IForeignExchange foreignExchange,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _customerRepository = customerRepository;
            _foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

            var allProductPrices =
                await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            var conversionRates = _foreignExchange.GetConversionRates();

            var orderProductsData = command
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();

            var orderId = customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                command.Currency,
                conversionRates);

            return orderId.Value;
        }
    }
}