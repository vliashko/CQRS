using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Data;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Customers.Orders;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Customers.IntegrationHandlers
{
    public class MarkCustomerAsWelcomedCommandHandler : ICommandHandler<MarkCustomerAsWelcomedCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository, 
            ISqlConnectionFactory sqlConnectionFactory, IEmailSender emailSender)
        {
            _customerRepository = customerRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Customer].[Email] " +
                               "FROM orders.v_Customers AS [Customer] " +
                               "WHERE [Customer].[Id] = @Id";

            var customerEmail = await connection.QueryFirstAsync<string>(sql,
                new
                {
                    Id = command.CustomerId.Value
                });

            var emailMessage = new EmailMessage(
                customerEmail,
                "Welcome on our site!",
                "Welcome!");

            await _emailSender.SendEmailAsync(emailMessage);

            customer.MarkAsWelcomedByEmail();

            return Unit.Value;
        }
    }
}