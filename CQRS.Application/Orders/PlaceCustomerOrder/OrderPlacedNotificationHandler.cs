using CQRS.Application.Configuration.Data;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Customers.Orders;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        private readonly IEmailSender _emailSender;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public OrderPlacedNotificationHandler(
            IEmailSender emailSender,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _emailSender = emailSender;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Customer].[Email] " +
                               "FROM orders.v_Customers AS [Customer] " +
                               "WHERE [Customer].[Id] = @Id";

            var customerEmail = await connection.QueryFirstAsync<string>(sql,
                new
                {
                    Id = request.CustomerId.Value
                });

            var emailMessage = new EmailMessage(
                customerEmail,
                OrderNotificationsService.GetOrderEmailConfirmationDescription(request.OrderId),
                "Order Information");

            await _emailSender.SendEmailAsync(emailMessage);
        }
    }
}