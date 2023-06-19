using Hahn.Customers.Domain.Aggregates;
using MediatR;

namespace Hahn.Customers.Infrastructure.CQRS.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string BankAccountNumber { get; set; }
    }
}
