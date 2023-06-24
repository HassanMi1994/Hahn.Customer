using Hahn.Customers.Domain;
using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.CQRS.Queries;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries.Handlers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginationResponse<IEnumerable<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<PaginationResponse<IEnumerable<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomersAsync(request.PageSize, request.PageNumber,request.Search);
        }
    }
}