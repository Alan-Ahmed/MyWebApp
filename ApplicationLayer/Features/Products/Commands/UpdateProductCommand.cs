using MediatR;
using MyWebApp.ApplicationLayer.Common;


namespace MyWebApp.ApplicationLayer.Features.Products.Commands
{
    public record UpdateProductCommand(
        int Id,
        string Name,
        decimal Price,
        string? Description = null
    ) : IRequest<OperationResult<ProductDTO>>;
}
