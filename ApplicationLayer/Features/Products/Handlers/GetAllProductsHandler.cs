using MediatR;
using AutoMapper;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Queries;
using MyWebApp.ApplicationLayer.Interfaces;

namespace MyWebApp.ApplicationLayer.Features.Products.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, OperationResult<List<ProductDTO>>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<ProductDTO>>(list);
            return OperationResult.Ok(dtos);
        }
    }
}
