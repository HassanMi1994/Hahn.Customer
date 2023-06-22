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

        [Route("customers")]
        [HttpGet]
        public async Task<IEnumerable<Customer>> Put()
        {
            Thread.Sleep(700);
            return await _mediator.Send(new GetCustomersQuery());
        }

        [Route("customers/{customerId}")]
        [HttpGet]
        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(customerId));
        }

        [Route("customers/create")]
        [HttpPost]
        public async Task<Customer> Create([FromBody] CreateCustomerCommand command)
        {
            return await _mediator.Send(command);
        }

        [Route("customers/put")]
        [HttpPut]
        public async Task<Customer> Put([FromBody] UpdateCustomerCommand cmd)
        {
            return await _mediator.Send(cmd);
        }

        [Route("customers/delete/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _mediator.Send(new DeleteCustomerCommand(id));
        }
    }
}
