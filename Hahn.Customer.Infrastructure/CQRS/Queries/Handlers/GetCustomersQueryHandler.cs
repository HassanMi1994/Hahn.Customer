using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.CQRS.Queries;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries.Handlers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return _customerRepository.GetCustomersAsync();
        }
    }
}