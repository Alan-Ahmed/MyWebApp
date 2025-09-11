using MediatR;
using AutoMapper;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Commands;
using MyWebApp.ApplicationLayer.Interfaces;


namespace MyWebApp.ApplicationLayer.Features.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, OperationResult<ProductDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
                return OperationResult.Fail<ProductDTO>("Product not found");

            existing.Name = request.Name;
            existing.Price = request.Price;
            existing.Description = request.Description;

            var updated = await _repo.UpdateAsync(existing);
            return OperationResult.Ok(_mapper.Map<ProductDTO>(updated!));
        }
    }
}
