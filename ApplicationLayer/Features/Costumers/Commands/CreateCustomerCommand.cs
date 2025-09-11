using MediatR;
using MyWebApp.ApplicationLayer.Common;
using ApplicationLayer.DTOs;

namespace MyWebApp.ApplicationLayer.Features.Customers.Commands
{
    public record CreateCustomerCommand(string Name, string Email)
        : IRequest<OperationResult<CustomerDto>>;
}
