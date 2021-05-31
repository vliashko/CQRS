using CQRS.Domain.Customers;
using CQRS.Domain.Payments;
using CQRS.Domain.Products;
using CQRS.Infrastructure.Processing.InternalCommands;
using CQRS.Infrastructure.Processing.Outbox;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Database
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
