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

        [Route("customers/page-size/{pageSize}/page-number/{pageNumber}/search/{search}")]
        [HttpGet]
        public async Task<PaginationResponse<IEnumerable<Customer>>> GetCustomers(int pageSize, int pageNumber,string search)
        {
            Thread.Sleep(700);
            return await _mediator.Send(new GetCustomersQuery(pageSize, pageNumber,search));
        }

        [Route("customers/page-size/{pageSize}/page-number/{pageNumber}")]
        [HttpGet]
        public async Task<PaginationResponse<IEnumerable<Customer>>> GetCustomersWithoutSearch(int pageSize, int pageNumber)
        {
            Thread.Sleep(700);
            return await _mediator.Send(new GetCustomersQuery(pageSize, pageNumber, ""));
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

        [Route("customer/adddata")]
        [HttpGet]
        public async Task<bool> Add1000Items()
        {
            var rnd = new Random(200);
            for (int i = 1; i < 200; i++)
            {
                var cust = new CreateCustomerCommand
                {
                    BankAccountNumber = "2334543245834593",
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Email = i * 10 * 35 + "email@gmail.com",
                    FirstName = "h" + +i + "xsld" + i * 2,
                    LastName = "najafi" + i
                };
                await _mediator.Send(cust);
            }
            return true;
        }
    }
}
