using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.FluentValidations;

namespace Hahn.Customers.Infrastructure.CQRS.Commands.Handlers
{
    public class CreateUpdateBase
    {
        protected readonly ICustomerRepository _customerRepository;

        public CreateUpdateBase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ThrowExcpetionIfEmailExist(Customer customer)
        {
            var result = await _customerRepository.DoesCustomerEmailExistAsync(customer);
            if (result.exist)
            {
                throw new CustomValidationException("email", $"There is a customer ({result.emailOwner}) which owns this email address.");
            }
        }
    }
}
