using CQRS.Application.Configuration.Commands;
using CQRS.Domain.Payments.Braintree;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Payments.GenerateToken
{
    public class GenerateTokenCommandHandler : ICommandHandler<GenerateTokenCommand, TokenDto>
    {
        private readonly BraintreeConfiguration _config = new BraintreeConfiguration();

        public async Task<TokenDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var gateway = _config.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            return new TokenDto { Token = clientToken };
        }
    }
}
