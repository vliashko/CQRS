using CQRS.Application.Configuration;
using System;

namespace CQRS.IntegrationTests.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public Guid CorrelationId { get; set; }

        public bool IsAvailable { get; set; }
    }
}