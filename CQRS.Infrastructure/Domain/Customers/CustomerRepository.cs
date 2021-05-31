using CQRS.Domain.Customers;
using CQRS.Domain.Customers.Orders;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrdersContext _context;

        public CustomerRepository(OrdersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetByIdAsync(CustomerId id)
        {
            return await _context.Customers
                .IncludePaths(
                    CustomerEntityTypeConfiguration.OrdersList,
                    CustomerEntityTypeConfiguration.OrderProducts)
                .SingleAsync(x => x.Id == id);
        }
    }
}