using MediatR;
using AutoMapper;
using ApplicationLayer.DTOs;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Queries;
using MyWebApp.ApplicationLayer.Interfaces;

namespace MyWebApp.ApplicationLayer.Features.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, OperationResult<ProductDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null)
                return OperationResult.Fail<ProductDTO>("Product not found");

            return OperationResult.Ok(_mapper.Map<ProductDTO>(entity));
        }
    }
}
