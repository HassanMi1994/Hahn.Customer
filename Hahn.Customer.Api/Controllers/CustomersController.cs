using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.CQRS.Commands;
using Hahn.Customers.Infrastructure.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;


        [Route("/Customers")]
        [HttpGet]
        public async Task<IEnumerable<Customer>> Put()
        {
            return await _mediator.Send(new GetCustomersQuery());
        }

        [Route("/Customers/{customerId}")]
        [HttpGet]
        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(customerId));
        }

        [Route("Create")]
        [HttpPost]
        public async Task<Customer> Create([FromBody] CreateCustomerCommand command)
        {
            return await _mediator.Send(command);
        }

        [Route("Put")]
        [HttpPut]
        public async Task<Customer> Put([FromBody] UpdateCustomerCommand cmd)
        {
            return await _mediator.Send(cmd);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _mediator.Send(new DeleteCustomerCommand(id));
        }
    }
}
