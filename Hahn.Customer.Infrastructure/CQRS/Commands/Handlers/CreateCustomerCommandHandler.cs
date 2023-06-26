using Hahn.Customers.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class CreateCustomerCommandHandler : CreateUpdateBase, IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
            : base(customerRepository)
        {
            _logger = logger;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.FirstName, request.LastName, request.DateOfBirth, request.Email, request.BankAccountNumber);
            await ThrowExcpetionIfEmailExist(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();
            return customer;
        }
    }
}
