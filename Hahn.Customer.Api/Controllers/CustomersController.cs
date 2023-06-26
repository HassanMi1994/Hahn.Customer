using Hahn.Customers.Domain;
using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.CQRS.Commands;
using Hahn.Customers.Infrastructure.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.Customers.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("customers/page-size/{pageSize}/page-number/{pageNumber}/search/{search}")]
        public async Task<PaginationResponse<IEnumerable<Customer>>> GetCustomers(int pageSize, int pageNumber, string search)
        {
            Thread.Sleep(700);
            return await _mediator.Send(new GetCustomersQuery(pageSize, pageNumber, search));
        }

        [HttpGet]
        [Route("customers/page-size/{pageSize}/page-number/{pageNumber}")]
        public async Task<PaginationResponse<IEnumerable<Customer>>> GetCustomersWithoutSearch(int pageSize, int pageNumber)
        {
            Thread.Sleep(700);
            return await _mediator.Send(new GetCustomersQuery(pageSize, pageNumber, ""));
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public async Task<Customer> GetCustomerById(int customerId) => await _mediator.Send(new GetCustomerByIdQuery(customerId));

        [HttpPost]
        [Route("customers/create")]
        public async Task<Customer> Create([FromBody] CreateCustomerCommand command) => await _mediator.Send(command);


        [HttpPut]
        [Route("customers/put")]
        public async Task<Customer> Put([FromBody] UpdateCustomerCommand cmd) => await _mediator.Send(cmd);


        [HttpDelete]
        [Route("customers/delete/{id}")]
        public async Task<bool> Delete(int id) => await _mediator.Send(new DeleteCustomerCommand(id));

    }
}
