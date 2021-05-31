using CQRS.Application.Orders.PlaceCustomerOrder;
using System.Reflection;

namespace CQRS.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
    }
}