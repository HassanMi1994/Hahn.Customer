using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries.Handlers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.GetByIdAsync(request.Id);
            if (customer == null) throw new InvalidOperationException("Customer does not exist");
            return customer;
        }
    }
}
