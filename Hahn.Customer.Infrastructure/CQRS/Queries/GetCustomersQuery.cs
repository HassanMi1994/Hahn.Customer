using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<Customer>>
    {
        public GetCustomersQuery() { }
    }
}
