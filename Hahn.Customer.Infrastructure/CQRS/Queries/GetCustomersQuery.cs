using Hahn.Customers.Domain;
using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Queries
{
    public class GetCustomersQuery : IRequest<PaginationResponse<IEnumerable<Customer>>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Search { get; set; }

        public GetCustomersQuery(int pageSize, int pageNumber, string search)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Search = search;
        }
    }
}
