using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Features.Customers.Commands
{
    public record DeleteCustomerCommand(int Id) : IRequest<OperationResult<bool>>;
}
