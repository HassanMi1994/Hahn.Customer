using Hahn.Customers.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : CreateUpdateBase, IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
            : base(customerRepository)
        {
            _logger = logger;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerInDb = await _customerRepository.GetByIdAsync(request.Id);
            if (customerInDb == null) throw new Exception("This customer id does not exist");
            customerInDb.UpdateValues(request.Id, request.FirstName, request.LastName, request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);
            await ThrowExcpetionIfEmailExist(customerInDb);
            await _customerRepository.UnitOfWork.SaveEntitiesAsync();
            return customerInDb;
        }
    }
}
