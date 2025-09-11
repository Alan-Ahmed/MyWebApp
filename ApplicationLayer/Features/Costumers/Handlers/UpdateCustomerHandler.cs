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
    public class UpdateCustomerHandler
        : IRequestHandler<UpdateCustomerCommand, OperationResult<CustomerDto>>
    {
        private readonly IRepository<Customer> _repo;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(IRepository<Customer> repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<OperationResult<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null) return OperationResult.Fail<CustomerDto>("Customer not found");

            entity.Name = request.Name;
            entity.Email = request.Email;
            await _repo.UpdateAsync(entity);

            return OperationResult.Ok(_mapper.Map<CustomerDto>(entity));
        }
    }
}
