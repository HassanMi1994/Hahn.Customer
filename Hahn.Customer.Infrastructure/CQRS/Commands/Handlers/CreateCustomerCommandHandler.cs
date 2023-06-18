using Hahn.Customers.Domain.Events;
using Hahn.Customers.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.FirstName, request.LastName, request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);
            _customerRepository.Add(customer);
            customer.AddDomainEvent(new CustomerCreatedDomainEvent(customer.Id, customer.FirstName, customer.LastName, customer.DateOfBirth, customer.PhoneNumber, customer.Email, customer.BankAccountNumber));
            int howManyRowAffected = await _customerRepository.UnitOfWork.SaveChangesAsync();
            return customer;
        }
    }
}
