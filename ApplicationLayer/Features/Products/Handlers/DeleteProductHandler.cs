using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Commands;
using MyWebApp.ApplicationLayer.Interfaces;

namespace MyWebApp.ApplicationLayer.Features.Products.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, OperationResult<bool>>
    {
        private readonly IProductRepository _repo;

        public DeleteProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<bool>> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var ok = await _repo.DeleteAsync(request.Id);
            return ok
                ? OperationResult.Ok(true)
                : OperationResult.Fail<bool>("Product not found");
        }
    }
}
