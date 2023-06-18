using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
