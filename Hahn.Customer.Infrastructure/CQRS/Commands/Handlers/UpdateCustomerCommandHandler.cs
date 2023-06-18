using Hahn.Customers.Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerInDb = await _customerRepository.GetByIdAsync(request.Id);
            if (customerInDb == null) throw new Exception("This customer id does not exist");
            UpdateValuesIfChanged(request, customerInDb);
            await _customerRepository.UnitOfWork.SaveEntitiesAsync();
            return customerInDb;
        }

        private static void UpdateValuesIfChanged(UpdateCustomerCommand request, Customer customerInDb)
        {
            //todo: write the update process, and raise the appropriate event!
        }
    }
}
