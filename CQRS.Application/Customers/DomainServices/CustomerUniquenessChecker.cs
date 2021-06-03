using CQRS.Application.Configuration.Data;
using CQRS.Domain.Customers;
using Dapper;

namespace CQRS.Application.Customers.DomainServices
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CustomerUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string customerEmail)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT TOP 1 1" +
                         "FROM [orders].[Customers] AS [Customer] " +
                         "WHERE [Customer].[Email] = @Email";
            var customersNumber = connection.QuerySingleOrDefault<int?>(sql,
                            new
                            {
                                Email = customerEmail
                            });

            return !customersNumber.HasValue;
        }
    }
}