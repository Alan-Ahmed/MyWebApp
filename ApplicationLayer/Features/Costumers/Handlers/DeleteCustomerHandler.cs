using MediatR;
using MyWebApp.ApplicationLayer.Common;
using MyWebApp.ApplicationLayer.Features.Customers.Commands;
using MyWebApp.ApplicationLayer.Interfaces;
using MyWebApp.DomainLayer.Entities;
using MyWebApp.InfrastructureLayer.Repositories;

namespace MyWebApp.ApplicationLayer.Features.Customers.Handlers
{
    public class DeleteCustomerHandler
        : IRequestHandler<DeleteCustomerCommand, OperationResult<bool>>
    {
        private readonly IRepository<Customer> _repo;
        public DeleteCustomerHandler(IRepository<Customer> repo) { _repo = repo; }

        public async Task<OperationResult<bool>> Handle(DeleteCustomerCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity is null) return OperationResult.Fail<bool>("Customer not found");

            await _repo.DeleteAsync(entity);
            return OperationResult.Ok(true);
        }
    }
}
