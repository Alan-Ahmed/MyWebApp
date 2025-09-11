using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Features.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        string? Description = null
    ) : IRequest<OperationResult<ProductDTO>>;
}
