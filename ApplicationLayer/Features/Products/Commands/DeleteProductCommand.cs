using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Features.Products.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<OperationResult<bool>>;
}
