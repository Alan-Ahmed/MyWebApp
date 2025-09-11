using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Commands;
using MyWebApp.ApplicationLayer.Features.Products.Queries;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<OperationResult<List<ProductDTO>>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OperationResult<ProductDTO>>> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult<ProductDTO>>> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OperationResult<ProductDTO>>> Update(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(OperationResult.Fail<ProductDTO>("Id mismatch"));

            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OperationResult<bool>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
