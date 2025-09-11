using MediatR;
using AutoMapper;
using ApplicationLayer.DTOs;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Products.Commands;
using MyWebApp.ApplicationLayer.Interfaces;
using MyWebApp.DomainLayer.Entities;

namespace MyWebApp.ApplicationLayer.Features.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, OperationResult<ProductDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var entity = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };

            entity = await _repo.CreateAsync(entity); // SaveChangesAsync sker i repo
            var dto = _mapper.Map<ProductDTO>(entity); // dto.Id > 0
            return OperationResult.Ok(dto);
        }
    }
}
