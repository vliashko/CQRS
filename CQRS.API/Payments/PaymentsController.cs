using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRS.API.Payments
{
    [Route("api/customers")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("GenerateToken")]
        public async Task<object> GenerateToken()
        {
            
        }
    }
}
