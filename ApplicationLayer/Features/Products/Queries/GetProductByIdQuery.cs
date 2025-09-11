using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<OperationResult<ProductDTO>>;
}
