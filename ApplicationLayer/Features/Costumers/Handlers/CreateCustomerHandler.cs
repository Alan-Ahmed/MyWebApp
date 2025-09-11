using ApplicationLayer.DTOs;
using AutoMapper;
using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Customers.Commands;
using MyWebApp.ApplicationLayer.Interfaces;
using MyWebApp.DomainLayer.Entities;
using MyWebApp.InfrastructureLayer.Repositories;

namespace MyWebApp.ApplicationLayer.Features.Customers.Handlers
{
    public class CreateCustomerHandler
        : IRequestHandler<CreateCustomerCommand, OperationResult<CustomerDto>>
    {
        private readonly IRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken ct)
        {
            var entity = new Customer { Name = request.Name, Email = request.Email };
            await _repo.AddAsync(entity);
            var dto = _mapper.Map<CustomerDto>(entity);
            return OperationResult.Ok(dto);
        }
    }
}
