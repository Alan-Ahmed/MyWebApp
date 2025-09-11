using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Customers.Commands;
using MyWebApp.ApplicationLayer.Features.Customers.Queries;
using ApplicationLayer.DTOs;

namespace MyWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<OperationResult<List<CustomerDto>>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllCustomersQuery(), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OperationResult<CustomerDto>>> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult<CustomerDto>>> Create([FromBody] CreateCustomerCommand cmd, CancellationToken ct)
        {
            var result = await _mediator.Send(cmd, ct);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OperationResult<CustomerDto>>> Update(int id, [FromBody] UpdateCustomerCommand cmd, CancellationToken ct)
        {
            if (id != cmd.Id) return BadRequest(OperationResult.Fail<CustomerDto>("Id mismatch"));
            var result = await _mediator.Send(cmd, ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OperationResult<bool>>> Delete(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
