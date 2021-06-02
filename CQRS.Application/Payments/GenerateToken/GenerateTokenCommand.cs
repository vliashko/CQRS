using CQRS.Application.Configuration.Commands;

namespace CQRS.Application.Payments.GenerateToken
{
    public class GenerateTokenCommand : CommandBase<TokenDto>
    {
        public GenerateTokenCommand()
        {
        }
    }
}
