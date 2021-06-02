using CQRS.Application.Payments.GenerateToken;
using CQRS.Application.Payments.PaymentSystem;
using CQRS.Domain.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CQRS.API.Payments
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GenerateToken")]
        public async Task<IActionResult> GenerateToken()
        {
            var token = await _mediator.Send(new GenerateTokenCommand());
            return Created(string.Empty, token.Token);
        }

        [HttpPost("{paymentId}")]
        public async Task<IActionResult> Create(Guid paymentId, [FromBody] PaymentRequest payment)
        {
            await _mediator.Send(new PaymentCommand(new PaymentId(paymentId), payment));
            return Ok(string.Empty);
        }
    }
}
