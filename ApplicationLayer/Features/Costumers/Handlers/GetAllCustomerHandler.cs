using ApplicationLayer.DTOs;
using AutoMapper;
using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Customers.Queries;
using MyWebApp.ApplicationLayer.Interfaces;
using MyWebApp.DomainLayer.Entities;
using MyWebApp.InfrastructureLayer.Repositories;

namespace MyWebApp.ApplicationLayer.Features.Customers.Handlers
{
    public sealed class GetAllCustomerHandler
        : IRequestHandler<GetAllCustomersQuery, OperationResult<List<CustomerDto>>>
    {
        private readonly IRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(IRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CustomerDto>>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken)
        {
            var customers = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<CustomerDto>>(customers);
            return OperationResult.Ok(dtos);
        }
    }
}
