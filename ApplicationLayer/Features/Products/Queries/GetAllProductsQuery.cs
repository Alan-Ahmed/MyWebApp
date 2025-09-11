using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<OperationResult<List<ProductDTO>>>;
}
