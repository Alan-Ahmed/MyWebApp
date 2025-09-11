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
    public sealed class GetCustomerByIdHandler
        : IRequestHandler<GetCustomerByIdQuery, OperationResult<CustomerDto>>
    {
        private readonly IRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandler(IRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<CustomerDto>> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null)
                return OperationResult.Fail<CustomerDto>("Customer not found");

            return OperationResult.Ok(_mapper.Map<CustomerDto>(entity));
        }
    }
}
