using MediatR;
using MyWebApp.ApplicationLayer.Common;
using ApplicationLayer.DTOs;

namespace MyWebApp.ApplicationLayer.Features.Customers.Queries
{
    public record GetCustomerByIdQuery(int Id) : IRequest<OperationResult<CustomerDto>>;
}
